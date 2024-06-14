using Shopfloor.Features.Admin.Suppliers.Commands;
using Shopfloor.Interfaces;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Admin.Suppliers
{
    internal sealed class SuppliersListViewModel : ViewModelBase, IInputForm<Supplier>
    {
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        private readonly ObservableCollection<Supplier> _suppliers = [];
        private readonly SuppliersStore _suppliersStore;
        private bool _isEdit;
        private string _name = string.Empty;
        private string _searchText = string.Empty;
        private Supplier? _selectedSupplier;
        private readonly SupplierProvider _supplierProvider;
        public SuppliersListViewModel(SupplierProvider supplierProvider, SuppliersStore suppliersStore)
        {
            _supplierProvider = supplierProvider;

            SupplierAddCommand = new SupplierAddCommand(this, _supplierProvider);
            SupplierEditCommand = new SupplierEditCommand(this, _supplierProvider);
            CleanFormCommand = new CleanFormCommand(this);

            _suppliersStore = suppliersStore;
            Task.Run(LoadData);
        }
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public ICommand CleanFormCommand { get; }
        public bool HasErrors => _propertyErrors.Count != 0;
        public bool IsEdit
        {
            get => _isEdit;
            set
            {
                _isEdit = value;
                OnPropertyChanged(nameof(IsEdit));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Suppliers.Filter = FilterList;
                OnPropertyChanged(nameof(SearchText));
            }
        }
        public Supplier? SelectedSupplier
        {
            get => _selectedSupplier;
            set
            {
                _selectedSupplier = value;
                if (value is null)
                {
                    IsEdit = false;
                    Name = string.Empty;
                }
                else
                {
                    Name = value.Name;
                    IsEdit = true;
                }

                OnPropertyChanged(nameof(SelectedSupplier));
            }
        }
        public ICommand SupplierAddCommand { get; }
        public ICommand SupplierEditCommand { get; }
        public ICollectionView Suppliers => CollectionViewSource.GetDefaultView(_suppliers);
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
            Name = string.Empty;
            IsEdit = false;
            SelectedSupplier = null;
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
        public Task LoadData()
        {
            Application.Current.Dispatcher.Invoke(_suppliers.Clear);

            List<Supplier> suppliers = _suppliersStore.Data;
            foreach (Supplier supplier in suppliers)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _suppliers.Add(supplier);
                    OnPropertyChanged(nameof(Suppliers));
                });
            }
            return Task.CompletedTask;
        }
        public void ReloadData()
        {
            _suppliersStore.Reload().Wait();
        }
        //Updates the list if value didn't exist, ie. after add
        public async Task UpdateData()
        {
            //await Task.Delay(5000);
            IEnumerable<Supplier> suppliers = await _supplierProvider.GetAll();
            foreach (Supplier supplier in suppliers)
            {
                if (_suppliers.FirstOrDefault(s => s.Id == supplier.Id) is null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _suppliers.Add(supplier);
                        OnPropertyChanged(nameof(Suppliers));
                    });
                }
            }
        }
        //Updates the list if value existed, ie. after edit
        public async Task UpdateData(Supplier supplierToRemove)
        {
            if (supplierToRemove.Id is null) return;
            Supplier supplierToAdd = await _supplierProvider.GetById((int)supplierToRemove.Id);
            if (_suppliers.FirstOrDefault(s => s.Id == supplierToRemove.Id) is not null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _suppliers.Remove(supplierToRemove);
                    _suppliers.Add(supplierToAdd);
                    OnPropertyChanged(nameof(Suppliers));
                });
            }
        }
        private bool FilterList(object obj)
        {
            if (obj is Supplier supplier)
            {
                return supplier.SearchValue.Contains(_searchText, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(IsDataValidate));
        }
    }
}