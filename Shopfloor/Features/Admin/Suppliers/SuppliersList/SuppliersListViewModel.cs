using Microsoft.Extensions.DependencyInjection;
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

namespace Shopfloor.Features.Admin.Suppliers.List
{
    public class SuppliersListViewModel : ViewModelBase, IInputForm<Supplier>
    {
        private readonly IServiceProvider _databaseServices;
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        private readonly ObservableCollection<Supplier> _suppliers = [];
        private readonly SuppliersStore _suppliersStore;
        private bool _isEdit;
        private string _name = string.Empty;
        private string _searchText = string.Empty;
        private Supplier? _selectedSupplier;
        public SuppliersListViewModel(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
            SupplierProvider provider = _databaseServices.GetRequiredService<SupplierProvider>();

            SupplierAddCommand = new SupplierAddCommand(this, provider);
            SupplierEditCommand = new SupplierEditCommand(this, provider);
            CleanFormCommand = new CleanFormCommand(this);

            _suppliersStore = _databaseServices.GetRequiredService<SuppliersStore>();
            Task.Run(() => LoadData(_databaseServices));
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
        public async Task LoadData(IServiceProvider databaseServices)
        {
            List<Task> tasks = [];
            Application.Current.Dispatcher.Invoke(_suppliers.Clear);
            if (!_suppliersStore.IsLoaded) tasks.Add(LoadSuppliers());

            if (tasks.Count > 0) await Task.WhenAll(tasks);

            IEnumerable<Supplier> suppliers = _suppliersStore.Data;
            foreach (Supplier supplier in suppliers)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _suppliers.Add(supplier);
                    OnPropertyChanged(nameof(Suppliers));
                });
            }
        }
        public Task LoadSuppliers()
        {
            _suppliersStore.Load();
            return Task.CompletedTask;
        }
        public void ReloadData()
        {
            _databaseServices.GetRequiredService<SuppliersStore>().Load();
        }
        //Updates the list if value didn't exist, ie. after add
        public async Task UpdateData()
        {
            //await Task.Delay(5000);
            SupplierProvider provider = _databaseServices.GetRequiredService<SupplierProvider>();
            IEnumerable<Supplier> suppliers = await provider.GetAll();
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
            SupplierProvider provider = _databaseServices.GetRequiredService<SupplierProvider>();
            Supplier supplierToAdd = await provider.GetById((int)supplierToRemove.Id);
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