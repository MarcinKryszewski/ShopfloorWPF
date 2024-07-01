using Shopfloor.Features.Admin.Parts.Commands;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Admin.Parts
{
    internal sealed class PartsAddViewModel : ViewModelBase, IInputForm<Part>
    {
        private readonly List<PartType> _partTypes;
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        private readonly List<Supplier> _suppliers = [];
        private readonly IDataStore<Part> _partStore;
        private string _details = string.Empty;
        private int? _index;
        private string _nameOriginal = string.Empty;
        private string _namePl = string.Empty;
        private string _number = string.Empty;
        private Supplier? _producer;
        private Supplier? _supplier;
        private PartType? _type;
        private string? _unit;
        public PartsAddViewModel(IDataStore<PartType> partTypeStore, IDataStore<Supplier> suppliersStore, IDataStore<Part> partStore, PartProvider partProvider, INavigationCommand<PartsListViewModel> returnCommand)
        {
            _partStore = partStore;

            ReturnCommand = returnCommand.Navigate();
            CleanFormCommand = new PartCleanFormCommand(this);
            AddPartCommand = new PartAddCommand(this, partProvider);

            _partTypes = partTypeStore.Data;
            _suppliers = suppliersStore.Data;

            PartTypes = CollectionViewSource.GetDefaultView(_partTypes);
            Suppliers = CollectionViewSource.GetDefaultView(_suppliers);
            Producers = CollectionViewSource.GetDefaultView(_suppliers);
        }
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public ICommand AddPartCommand { get; }
        public ICommand CleanFormCommand { get; }
        public bool HasErrors => _propertyErrors.Count != 0;
        public Visibility HasErrorVisibility => HasErrors ? Visibility.Collapsed : Visibility.Visible;
        public ICollectionView PartTypes { get; }
        public ICollectionView Producers { get; }
        public ICommand ReturnCommand { get; }
        public ICollectionView Suppliers { get; }
        public string Details
        {
            get => _details;
            set
            {
                _details = value;
                OnPropertyChanged(nameof(Details));
            }
        }
        public string? Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }
        public int? Index
        {
            get => _index;
            set
            {
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }
        public string NameOriginal
        {
            get => _nameOriginal;
            set
            {
                _nameOriginal = value;
                OnPropertyChanged(nameof(NameOriginal));
            }
        }
        public string NamePl
        {
            get => _namePl;
            set
            {
                _namePl = value;
                OnPropertyChanged(nameof(NamePl));
            }
        }
        public string Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        public Supplier? Producer
        {
            get => _producer;
            set
            {
                _producer = value;
                OnPropertyChanged(nameof(Producer));
            }
        }
        public int ProducerId => Producer?.Id ?? 0;
        public Supplier? Supplier
        {
            get => _supplier;
            set
            {
                _supplier = value;
                OnPropertyChanged(nameof(Supplier));
            }
        }
        public int? SupplierId => Supplier?.Id;
        public PartType? Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        public int TypeId => Type?.Id ?? 0;
        public void AddError(string propertyName, string errorMassage)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, []);
            }
            _propertyErrors[propertyName]?.Add(errorMassage);
            OnErrorsChanged(propertyName);
        }
        public void CleanForm()
        {
            NamePl = string.Empty;
            NameOriginal = string.Empty;
            Type = null;
            Index = null;
            Number = string.Empty;
            Details = string.Empty;
            Producer = null;
            Supplier = null;
            Unit = GlobalConstants.DefaultPartUnit;
        }
        /*public bool IsDataValidate(Part inputValue)
        {
            if (inputValue.RequiredInputValue.Length == 0)
            {
                ErrorMassage = "Wprowadź nazwę, nazwę producenta lub indeks";
                return false;
            };
            Part? part = _databaseServices.GetRequiredService<PartsStore>().Data.FirstOrDefault(p => p.Index == inputValue.Index);
            if (part is not null)
            {
                ErrorMassage = "Część o podanym indeksie już istnieje";
                return false;
            }
            if (inputValue.Index is not null && inputValue.Index?.ToString().Length != 8)
            {
                ErrorMassage = "Indeks powinien być liczbą o długości 8 znaków";
                return false;
            }
            return true;
        }*/
        public void ClearErrors(string? propertyName)
        {
            if (propertyName is null) return;
            _propertyErrors.Remove(propertyName);
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
        }
        public bool IsDataValidate => !HasErrors;
        public void ReloadData()
        {
            _partStore.Reload().Wait();
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(IsDataValidate));
        }
    }
}