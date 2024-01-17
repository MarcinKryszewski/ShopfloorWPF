using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Suppliers.Commands;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.ViewModels;
using System;
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
    public class SuppliersListViewModel : ViewModelBase
    {
        private string _name = string.Empty;
        private Supplier? _selectedSupplier;
        private readonly ObservableCollection<Supplier> _suppliers = new();
        private bool _isEdit;
        private readonly IServiceProvider _databaseServices;
        private string _errorMassage = string.Empty;
        private string _searchText = string.Empty;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
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
                    ErrorMassage = string.Empty;
                }

                OnPropertyChanged(nameof(SelectedSupplier));
            }
        }
        public ICollectionView Suppliers => CollectionViewSource.GetDefaultView(_suppliers);
        public bool IsEdit
        {
            get => _isEdit;
            set
            {
                _isEdit = value;
                OnPropertyChanged(nameof(IsEdit));
            }
        }
        public string ErrorMassage
        {
            get => _errorMassage;
            set
            {
                _errorMassage = value;
                OnPropertyChanged(nameof(ErrorMassage));
                OnPropertyChanged(nameof(HasErrorVisibility));
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

        public Visibility HasErrorVisibility
        {
            get => _errorMassage.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        public ICommand SupplierAddCommand { get; }
        public ICommand SupplierEditCommand { get; }
        public ICommand SupplierDeleteCommand { get; }
        public ICommand CleanFormCommand { get; }
        //public ICommand SupplierSelectCommand { get; }

        public SuppliersListViewModel(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
            SupplierProvider provider = _databaseServices.GetRequiredService<SupplierProvider>();

            SupplierAddCommand = new SupplierAddCommand(this, provider);
            SupplierEditCommand = new SupplierEditCommand(this, provider);
            SupplierDeleteCommand = new SupplierDeleteCommand(this, provider);
            CleanFormCommand = new CleanFormCommand(this);
            //SupplierSelectCommand = new SupplierSelectCommand(this);

            Task.Run(() =>
            {
                _ = LoadData(provider);
            });
        }

        public async Task LoadData(SupplierProvider provider)
        {
            _suppliers.Clear();
            IEnumerable<Supplier> suppliers = await provider.GetAll();
            foreach (Supplier supplier in suppliers)
            {
                //await Task.Delay(350);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _suppliers.Add(supplier);
                    OnPropertyChanged(nameof(Suppliers));
                });
            }
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
            //await Task.Delay(5000);
            SupplierProvider provider = _databaseServices.GetRequiredService<SupplierProvider>();
            Supplier supplierToAdd = await provider.GetById(supplierToRemove.Id);
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
        public void CleanForm()
        {
            Name = string.Empty;
            IsEdit = false;
            ErrorMassage = string.Empty;
            SelectedSupplier = null;
        }
        private bool FilterList(object obj)
        {
            if (obj is Supplier supplier)
            {
                return supplier.SearchValue.Contains(_searchText, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
        public bool IsValidateData(Supplier? supplier)
        {
            if (supplier is null)
            {
                ErrorMassage = "Podaj dostawcę";
                return false;
            }

            if (supplier.Name.Length == 0)
            {
                ErrorMassage = "Wpisz nazwę dostawcy";
                return false;
            }

            if (_suppliers.FirstOrDefault(s => string.Equals(s.Name, supplier.Name, StringComparison.CurrentCultureIgnoreCase)) is not null)
            {
                ErrorMassage = "Dostawca istnieje";
                return false;
            }

            ErrorMassage = string.Empty;
            return true;
        }
    }
}