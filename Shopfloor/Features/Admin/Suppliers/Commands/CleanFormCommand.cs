using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Suppliers.Commands
{
    internal sealed class CleanFormCommand : CommandBase
    {
        private readonly SuppliersListViewModel _viewModel;

        public CleanFormCommand(SuppliersListViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.CleanForm();
        }
    }
}