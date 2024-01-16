using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Suppliers.Commands;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.ViewModels;

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
                OnPropertyChanged(nameof(SelectedSupplier));
            }
        }
        public IEnumerable<Supplier> Suppliers => _suppliers;
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

        public Visibility HasErrorVisibility
        {
            get => _errorMassage.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        public ICommand SupplierAddCommand { get; }
        public ICommand SupplierEditCommand { get; }
        public ICommand SupplierDeleteCommand { get; }

        public SuppliersListViewModel(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
            SupplierProvider provider = _databaseServices.GetRequiredService<SupplierProvider>();

            SupplierAddCommand = new SupplierAddCommand(this, provider);
            SupplierEditCommand = new SupplierEditCommand(this, provider);
            SupplierDeleteCommand = new SupplierDeleteCommand(this, provider);

            _ = LoadData(provider);
        }

        private async Task LoadData(SupplierProvider provider)
        {
            _suppliers.Clear();
            IEnumerable<Supplier> suppliers = await provider.GetAll();
            foreach (Supplier supplier in suppliers)
            {
                _suppliers.Add(supplier);
            }
            OnPropertyChanged(nameof(Suppliers));
        }

        public async Task UpdateData()
        {
            SupplierProvider provider = _databaseServices.GetRequiredService<SupplierProvider>();
            IEnumerable<Supplier> suppliers = await provider.GetAll();
            foreach (Supplier supplier in suppliers)
            {
                if (_suppliers.FirstOrDefault(s => s.Id == supplier.Id) is null) _suppliers.Add(supplier);
            }
            OnPropertyChanged(nameof(Suppliers));
        }

        public void CleanForm()
        {
            _name = string.Empty;
            _isEdit = false;
            _errorMassage = string.Empty;

            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(ErrorMassage));
            OnPropertyChanged(nameof(HasErrorVisibility));
        }
    }
}