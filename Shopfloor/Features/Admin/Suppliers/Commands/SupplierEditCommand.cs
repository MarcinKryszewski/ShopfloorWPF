using System;
using Shopfloor.Features.Admin.Suppliers.List;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;


namespace Shopfloor.Features.Admin.Suppliers.Commands
{
    public class SupplierEditCommand : CommandBase
    {
        private readonly SuppliersListViewModel _viewModel;
        private readonly SupplierProvider _provider;

        public SupplierEditCommand(SuppliersListViewModel viewModel, SupplierProvider provider)
        {
            _viewModel = viewModel;
            _provider = provider;
        }

        public override void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}