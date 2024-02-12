using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.Commands;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Mechanic.Errands.ErrandPartsList
{
    internal sealed partial class ErrandPartsListViewModel : ViewModelBase
    {
        private readonly List<Part> _parts = [];
        private readonly IServiceProvider _databaseServices;
        private readonly SelectedErrandStore _errandStore;

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
        public ErrandPartsListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
            _errandStore = mainServices.GetRequiredService<SelectedErrandStore>();
            AddPartToListCommand = new ErrandAddPartCommand(this, _errandStore);
            RemovePartFromListCommand = new ErrandRemovePartCommand(this, _errandStore);
            _errandPartValidation = new(this);
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

            PartsStore partsStore = _databaseServices.GetRequiredService<PartsStore>();
            ErrandPartStore errandPartStore = _databaseServices.GetRequiredService<ErrandPartStore>();

            List<Task> tasks = [];
            if (!partsStore.IsLoaded) tasks.Add(LoadParts(partsStore));
            if (!errandPartStore.IsLoaded) tasks.Add(LoadErrandParts(errandPartStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);

            tasks.Clear();

            tasks.Add(FillPartsList(partsStore));
            tasks.Add(FillErrandPartsList(errandPartStore, partsStore));
            if (tasks.Count > 0) await Task.WhenAll(tasks);

            Application.Current.Dispatcher.Invoke
            (() =>
            {
                PartsAll.Refresh();
                PartsMachine.Refresh();
                ErrandParts.Refresh();
            });
        }
        private Task LoadParts(PartsStore partsStore)
        {
            partsStore.Load();
            return Task.CompletedTask;
        }
        private Task LoadErrandParts(ErrandPartStore errandPartStore)
        {
            errandPartStore.Load();
            return Task.CompletedTask;
        }
        private Task FillPartsList(PartsStore partsStore)
        {
            foreach (Part part in partsStore.Data)
            {
                _parts.Add(part);
            }
            return Task.CompletedTask;
        }
        private Task FillErrandPartsList(ErrandPartStore errandPartStore, PartsStore partsStore)
        {
            foreach (ErrandPart errandPart in errandPartStore.Data)
            {
                if (errandPart.Status == ErrandPart.PartStatuses[6]) continue;
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