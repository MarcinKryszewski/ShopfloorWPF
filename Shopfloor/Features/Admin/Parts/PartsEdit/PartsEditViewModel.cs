using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Features.Admin.Parts.Commands;
using Shopfloor.Features.Admin.Parts.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Admin.Parts
{
    internal sealed class PartsEditViewModel : ViewModelBase, IInputForm<Part>
    {
        private readonly IDataStore<Part> _partStore;
        private readonly List<PartType> _partTypes;
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        private readonly Part? _selectedPart;
        private readonly List<Supplier> _suppliers;
        private string _details = string.Empty;
        private int? _index;
        private string _nameOriginal = string.Empty;
        private string _namePl = string.Empty;
        private string _number = string.Empty;
        private Supplier? _producer;
        private Supplier? _supplier;
        private PartType? _type;
        private string _unit = GlobalConstants.DefaultPartUnit;
        public PartsEditViewModel(
            INavigationCommand<PartsListViewModel> returnCommand,
            SelectedPartStore selectedPartStore,
            IDataStore<PartType> partTypeStore,
            IDataStore<Supplier> suppliersStore,
            IDataStore<Part> partStore,
            IProvider<Part> partProvider)
        {
            _selectedPart = selectedPartStore.Part;
            _partStore = partStore;

            ReturnCommand = returnCommand.Navigate();
            CleanFormCommand = new PartCleanFormCommand(this);
            EditPartCommand = new PartEditCommand(this, partProvider);

            _partTypes = partTypeStore.Data;
            _suppliers = suppliersStore.Data;

            Suppliers = CollectionViewSource.GetDefaultView(_suppliers);
            Producers = CollectionViewSource.GetDefaultView(_suppliers);
            PartTypes = CollectionViewSource.GetDefaultView(_partTypes);

            SetupForm();
        }
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public ICommand CleanFormCommand { get; }
        public string Details
        {
            get => _details;
            set
            {
                _details = value;
                OnPropertyChanged(nameof(Details));
            }
        }
        public ICommand EditPartCommand { get; }
        public bool HasErrors => _propertyErrors.Count != 0;
        public int? Id => _selectedPart?.Id;
        public int? Index
        {
            get => _index;
            set
            {
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }
        public bool IsDataValidate => !HasErrors;
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
        public PartType? PartType
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(PartType));
            }
        }
        public ICollectionView PartTypes { get; }
        public Supplier? Producer
        {
            get => _producer;
            set
            {
                _producer = value;
                OnPropertyChanged(nameof(Producer));
            }
        }
        public int? ProducerId => Producer?.Id;
        public ICollectionView Producers { get; }
        public ICommand ReturnCommand { get; }
        public Part? SelectedPart => _selectedPart;
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
        public ICollectionView Suppliers { get; }
        public int? TypeId => PartType?.Id;
        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }
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
            SetupForm();
        }
        public void ClearErrors(string? propertyName)
        {
            if (propertyName is null)
            {
                return;
            }

            _propertyErrors.Remove(propertyName);
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
        }
        public void ReloadData()
        {
            _partStore.Reload().Wait();
        }
        public void SetupForm()
        {
            //Part? selectedPart = _mainServices.GetRequiredService<SelectedPartStore>().Part;
            if (_selectedPart is null)
            {
                return;
            }

            NamePl = _selectedPart.NamePl ?? string.Empty;
            NameOriginal = _selectedPart.NameOriginal;
            Index = _selectedPart.Index;
            Number = _selectedPart.ProducerNumber;
            Details = _selectedPart.Details;
            PartType = _selectedPart.PartType;
            Producer = _selectedPart.Producer;
            Supplier = _selectedPart.Supplier;
            Unit = _selectedPart.Unit ?? GlobalConstants.DefaultPartUnit;
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(IsDataValidate));
        }
    }
}