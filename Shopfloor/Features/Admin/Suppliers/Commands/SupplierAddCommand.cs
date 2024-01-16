using System;
using System.Collections.Generic;
using System.Linq;
using Shopfloor.Features.Admin.Suppliers.List;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;


namespace Shopfloor.Features.Admin.Suppliers.Commands
{
    public class SupplierAddCommand : CommandBase
    {
        private readonly SuppliersListViewModel _viewModel;
        private readonly SupplierProvider _provider;

        public SupplierAddCommand(SuppliersListViewModel viewModel, SupplierProvider provider)
        {
            _viewModel = viewModel;
            _provider = provider;
        }

        public override void Execute(object? parameter)
        {
            List<Supplier> suppliers = new(_viewModel.Suppliers);
            Supplier supplier = new(_viewModel.Name, true);

            if (suppliers.FirstOrDefault(s => s.Name == supplier.Name) is null)
            {
                _viewModel.CleanForm();
                _ = _provider.Create(supplier);
                _ = _viewModel.UpdateData();
            }

        }
    }
}