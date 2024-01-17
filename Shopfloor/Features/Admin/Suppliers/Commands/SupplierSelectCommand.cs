using Shopfloor.Features.Admin.Suppliers.List;
using Shopfloor.Shared.Commands;
using System;

namespace Shopfloor.Features.Admin.Suppliers.Commands
{
    public class SupplierSelectCommand : CommandBase
    {
        private readonly SuppliersListViewModel _viewModel;

        public SupplierSelectCommand(SuppliersListViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
