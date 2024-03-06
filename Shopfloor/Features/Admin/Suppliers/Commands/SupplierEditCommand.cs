using Shopfloor.Features.Admin.Suppliers.List;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Shared.Commands;
using System.Threading.Tasks;

namespace Shopfloor.Features.Admin.Suppliers.Commands
{
    internal sealed class SupplierEditCommand : CommandBase
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
            if (_viewModel.SelectedSupplier is null) return;
            Supplier selectedSupplier = _viewModel.SelectedSupplier;
            if (selectedSupplier.Id is null) return;

            Supplier supplier = new(
                (int)selectedSupplier.Id,
                _viewModel.Name,
                selectedSupplier.IsActive);

            _ = _provider.UpdateAmount(supplier);

            Task.Run(() => _viewModel.UpdateData(selectedSupplier));

            _viewModel.CleanForm();
        }
    }
}