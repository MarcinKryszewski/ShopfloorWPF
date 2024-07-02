using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartModel.Store.Combine;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Utilities.CustomList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Mechanic.Errands
{
    internal sealed partial class ErrandPartsListViewModel : ViewModelBase
    {
        private List<Part> _parts = [];
        private readonly SelectedErrandStore _errandStore;
        private readonly IDataStore<Part> _partStore;
        private readonly IDataStore<ErrandPart> _errandPartStore;
        private readonly ICombinerManager<Part> _partCombiner;
        private string _searchText = string.Empty;
        private ErrandCreatorData? _errandCreatorData;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                PartsAll.Filter = FilterParts;
                OnPropertyChanged(nameof(SearchText));
            }
        }
        private bool FilterParts(object obj)
        {
            if (obj is Part part)
            {
                return part.SearchValue.Contains(_searchText, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
        public int PartsAmount => ErrandData?.Errand.Parts.Count ?? 0;
        public SearchableModelList DisplayList { get; }
        public ICollectionView PartsAll => CollectionViewSource.GetDefaultView(_parts);
        public ICollectionView PartsMachine => CollectionViewSource.GetDefaultView(_parts);
        public ErrandCreatorData? ErrandData
        {
            get => _errandCreatorData;
            set => _errandCreatorData = value;
        }
        public ICollectionView ErrandParts
        {
            get
            {
                OnPropertyChanged(nameof(PartsAmount));
                // return CollectionViewSource.GetDefaultView(_errandStore.ErrandParts);
                return CollectionViewSource.GetDefaultView(ErrandData?.Errand.Parts ?? []);
            }
        }
        public ICommand AddPartToListCommand { get; }
        public ICommand RemovePartFromListCommand { get; }
        public Part? SelectedPart { get; set; }
        public ErrandPartsListViewModel(
            SelectedErrandStore selectedErrandStore,
            IDataStore<Part> partStore,
            IDataStore<ErrandPart> errandPartStore,
            ICombinerManager<Part> partCombiner)
        {
            _errandStore = selectedErrandStore;
            _partStore = partStore;
            _errandPartStore = errandPartStore;
            _partCombiner = partCombiner;

            AddPartToListCommand = new ErrandAddPartCommand(this);
            RemovePartFromListCommand = new ErrandRemovePartCommand(this, _errandStore);
            _errandPartValidation = new(this);
            Task.Run(LoadData);

            DisplayList = new(_parts, 15);
        }
        private Task LoadData()
        {
            Application.Current.Dispatcher.Invoke
            (() =>
            {
                _parts.Clear();
                _errandStore.ErrandParts.Clear();
            });

            _partCombiner.CombineAll().Wait();

            _parts = _partStore.Data;
            DisplayList.Data = _parts;


            Application.Current.Dispatcher.Invoke
            (() =>
            {
                PartsAll.Refresh();
                PartsMachine.Refresh();
                ErrandParts.Refresh();
            });

            return Task.CompletedTask;
        }

        // private async Task LoadData()
        // {
        //     Application.Current.Dispatcher.Invoke
        //     (() =>
        //     {
        //         _parts.Clear();
        //         _errandStore.ErrandParts.Clear();
        //     });

        //     IDataStore<Part> partsStore = _partStore;
        //     IDataStore<ErrandPart> errandPartStore = _errandPartStore;

        //     //await LoadStores(partsStore, errandPartStore);
        //     //await CombineData(partsStore, errandPartStore);
        //     await FillLists(partsStore, errandPartStore);

        //     Application.Current.Dispatcher.Invoke
        //     (() =>
        //     {
        //         //PartsAll.Refresh();
        //         PartsMachine.Refresh();
        //         ErrandParts.Refresh();
        //     });
        //     await DisplayList.ReloadSourceData();
        // }
        public async Task FillLists()
        {
            List<Task> tasks = [];
            tasks.Add(FillPartsList());
            tasks.Add(FillErrandPartsList());
            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private Task FillPartsList()
        {
            _parts.AddRange(_partStore.Data);
            /*foreach (Part part in partsStore.Data)
            {
                _parts.Add(part);
            }*/
            return Task.CompletedTask;
        }
        private Task FillErrandPartsList()
        {
            foreach (ErrandPart errandPart in _errandPartStore.Data)
            {
                if (errandPart.LastStatusValue == 6) continue;
                if (errandPart.ErrandId == _errandStore.SelectedErrand?.Id)
                {
                    errandPart.Part = _partStore.Data.First(p => p.Id == errandPart.PartId);
                    _errandStore.ErrandParts.Add(errandPart);
                }
            }
            return Task.CompletedTask;
        }
    }
    internal sealed partial class ErrandPartsListViewModel : IInputForm<ErrandPart>
    {
        private readonly ErrandPartValidation _errandPartValidation;
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        public bool IsDataValidate
        {
            get
            {
                foreach (ErrandPart errandPart in _errandStore.ErrandParts)
                {
                    _errandPartValidation.ValidateAmount(nameof(errandPart.Amount), errandPart.Amount);
                    _errandPartValidation.ValidateObjectAmount(errandPart, errandPart.Amount);
                }
                return !HasErrors;
            }
        }
        public bool HasErrors => _propertyErrors.Count != 0;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public void AddError(string propertyName, string errorMassage)
        {
            if (!_propertyErrors.TryGetValue(propertyName, out List<string>? value))
            {
                value = [];
                _propertyErrors.Add(propertyName, value);
            }
            value?.Add(errorMassage);
            OnErrorsChanged(propertyName);
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(IsDataValidate));
        }
        public void CleanForm()
        {
            throw new NotImplementedException();
        }

        public void ClearErrors(string? propertyName)
        {
            if (propertyName is null)
            {
                _propertyErrors.Clear();
                return;
            }
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
        }
        public void ReloadData()
        {
            throw new NotImplementedException();
        }
    }
}