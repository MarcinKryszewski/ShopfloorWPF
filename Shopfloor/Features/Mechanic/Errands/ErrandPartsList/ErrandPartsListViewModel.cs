using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.PartModel;
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
        private readonly List<Part> _parts = [];
        private readonly SelectedErrandStore _errandStore;
        private readonly PartStore _partStore;
        private readonly ErrandPartStore _errandPartStore;
        private string _searchText = string.Empty;
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
        public int PartsAmount => _errandStore.ErrandParts.Count;
        public SearchableModelList DisplayList { get; }
        public ICollectionView PartsAll => CollectionViewSource.GetDefaultView(_parts);
        public ICollectionView PartsMachine => CollectionViewSource.GetDefaultView(_parts);
        public ICollectionView ErrandParts
        {
            get
            {
                OnPropertyChanged(nameof(PartsAmount));
                return CollectionViewSource.GetDefaultView(_errandStore.ErrandParts);
            }
        }
        public ICommand AddPartToListCommand { get; }
        public ICommand RemovePartFromListCommand { get; }
        public Part? SelectedPart { get; set; }
        public ErrandPartsListViewModel(SelectedErrandStore selectedErrandStore, PartStore partStore, ErrandPartStore errandPartStore)
        {
            _errandStore = selectedErrandStore;
            _partStore = partStore;
            _errandPartStore = errandPartStore;

            AddPartToListCommand = new ErrandAddPartCommand(this, _errandStore);
            RemovePartFromListCommand = new ErrandRemovePartCommand(this, _errandStore);
            _errandPartValidation = new(this);
            DisplayList = new(_parts, 15);
            Task.Run(LoadData);
        }
        private async Task LoadData()
        {
            Application.Current.Dispatcher.Invoke
            (() =>
            {
                _parts.Clear();
                _errandStore.ErrandParts.Clear();
            });

            PartStore partsStore = _partStore;
            ErrandPartStore errandPartStore = _errandPartStore;

            //await LoadStores(partsStore, errandPartStore);
            //await CombineData(partsStore, errandPartStore);
            await FillLists(partsStore, errandPartStore);

            Application.Current.Dispatcher.Invoke
            (() =>
            {
                //PartsAll.Refresh();
                PartsMachine.Refresh();
                ErrandParts.Refresh();
            });
            await DisplayList.ReloadSourceData();
        }
        public async Task FillLists(PartStore partsStore, ErrandPartStore errandPartStore)
        {
            List<Task> tasks = [];
            tasks.Add(FillPartsList(partsStore));
            tasks.Add(FillErrandPartsList(errandPartStore, partsStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private Task FillPartsList(PartStore partsStore)
        {
            _parts.AddRange(partsStore.Data);
            /*foreach (Part part in partsStore.Data)
            {
                _parts.Add(part);
            }*/
            return Task.CompletedTask;
        }
        private Task FillErrandPartsList(ErrandPartStore errandPartStore, PartStore partsStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                if (errandPart.LastStatusValue == 6) continue;
                if (errandPart.ErrandId == _errandStore.SelectedErrand?.Id)
                {
                    errandPart.Part = partsStore.Data.First(p => p.Id == errandPart.PartId);
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