using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Parts.Commands;
using Shopfloor.Features.Admin.Parts.Stores;
using Shopfloor.Interfaces;

using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Admin.Parts.Edit
{
    internal sealed class PartsEditViewModel : ViewModelBase, IInputForm<Part>
    {
        private readonly IServiceProvider _databaseServices;
        private readonly IServiceProvider _mainServices;
        private readonly ObservableCollection<PartType> _partTypes = [];
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        private readonly Part? _selectedPart;
        private readonly ObservableCollection<Supplier> _suppliers = [];
        #region modelFields
        private string _details = string.Empty;
        private int? _index;
        private string _nameOriginal = string.Empty;
        private string _namePl = string.Empty;
        private string _number = string.Empty;
        private Supplier? _producer;
        private Supplier? _supplier;
        private PartType? _type;
        private string _unit = "SZT";
        #endregion modelFields
        public PartsEditViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _mainServices = mainServices;
            _databaseServices = databaseServices;

            _selectedPart = _mainServices.GetRequiredService<SelectedPartStore>().Part;

            //ReturnCommand = new NavigateCommand<PartsListViewModel>(_mainServices.GetRequiredService<NavigationService<PartsListViewModel>>());
            CleanFormCommand = new PartCleanFormCommand(this);
            EditPartCommand = new PartEditCommand(this, _databaseServices);

            _partTypes = new(_databaseServices.GetRequiredService<PartTypeStore>().GetData);
            _suppliers = new(_databaseServices.GetRequiredService<SuppliersStore>().GetData);

            Suppliers = CollectionViewSource.GetDefaultView(_suppliers);
            Producers = CollectionViewSource.GetDefaultView(_suppliers);
            PartTypes = CollectionViewSource.GetDefaultView(_partTypes);

            SetupForm();
        }
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public ICommand CleanFormCommand { get; }
        public ICommand EditPartCommand { get; }
        public bool HasErrors => _propertyErrors.Count != 0;
        public ICollectionView PartTypes { get; }
        public ICollectionView Producers { get; }
        public ICommand ReturnCommand { get; }
        public Part? SelectedPart => _selectedPart;
        public ICollectionView Suppliers { get; }
        #region model properties
        public string Details
        {
            get => _details;
            set
            {
                _details = value;
                OnPropertyChanged(nameof(Details));
            }
        }
        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }
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
        public int? TypeId => PartType?.Id;
        #endregion model properties
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
            _databaseServices.GetRequiredService<PartStore>().Load();
        }
        public void SetupForm()
        {
            //Part? selectedPart = _mainServices.GetRequiredService<SelectedPartStore>().Part;
            if (_selectedPart is null) return;

            NamePl = _selectedPart.NamePl;
            NameOriginal = _selectedPart.NameOriginal;
            Index = _selectedPart.Index;
            Number = _selectedPart.Number;
            Details = _selectedPart.Details;
            PartType = _selectedPart.Type;
            Producer = _selectedPart.Producer;
            Supplier = _selectedPart.Supplier;
            Unit = _selectedPart.Unit;
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(IsDataValidate));
        }
    }
}