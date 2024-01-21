using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Parts.Commands;
using Shopfloor.Features.Admin.Parts.List;
using Shopfloor.Interfaces;
using Shopfloor.Models;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores.DatabaseDataStores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Admin.Parts.Add
{
    public class PartsAddViewModel : ViewModelBase, IInputForm<Part>
    {
        private readonly IServiceProvider _databaseServices;
        private readonly IServiceProvider _mainServices;
        private readonly ObservableCollection<PartType> _partTypes = [];
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
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
        #endregion modelFields
        public PartsAddViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _mainServices = mainServices;
            _databaseServices = databaseServices;

            ReturnCommand = new NavigateCommand<PartsListViewModel>(_mainServices.GetRequiredService<NavigationService<PartsListViewModel>>());
            CleanFormCommand = new PartCleanFormCommand(this);
            AddPartCommand = new PartAddCommand(this, _databaseServices);

            _partTypes = new(_databaseServices.GetRequiredService<PartTypesStore>().Data);
            _suppliers = new(_databaseServices.GetRequiredService<SuppliersStore>().Data);

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
        public PartType? Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        public int? TypeId => Type?.Id;
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
            NamePl = string.Empty;
            NameOriginal = string.Empty;
            Type = null;
            Index = null;
            Number = string.Empty;
            Details = string.Empty;
            Producer = null;
            Supplier = null;
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
        public void ClearErrors(string propertyName)
        {
            _propertyErrors.Remove(propertyName);
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName ?? "", null) ?? [];
        }
        public bool IsDataValidate => !HasErrors;
        public void ReloadData()
        {
            _databaseServices.GetRequiredService<PartsStore>().Load();
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(IsDataValidate));
        }
    }
}