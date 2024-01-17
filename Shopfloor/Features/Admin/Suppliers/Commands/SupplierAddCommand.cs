using Shopfloor.Features.Admin.Suppliers.List;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;
using System.Threading.Tasks;


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
            Supplier supplier = new(_viewModel.Name, true);

            _viewModel.CleanForm();

            Task.Run(async () =>
            {
                await _provider.Create(supplier);
                _ = _viewModel.UpdateData();
            });
        }
    }
}