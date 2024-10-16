using System.Threading.Tasks;
using Shopfloor.Interfaces;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Suppliers.Commands
{
    internal sealed class SupplierAddCommand : CommandBase
    {
        private readonly IProvider<Supplier> _provider;
        private readonly SuppliersListViewModel _viewModel;
        public SupplierAddCommand(SuppliersListViewModel viewModel, IProvider<Supplier> provider)
        {
            _viewModel = viewModel;
            _provider = provider;
        }

        public override void Execute(object? parameter)
        {
            Supplier supplier = new(_viewModel.Name, true);
            if (!_viewModel.IsDataValidate)
            {
                return;
            }

            _viewModel.CleanForm();

            Task.Run(async () =>
            {
                await _provider.Create(supplier);
                _ = _viewModel.UpdateData();
            });
        }
    }
}