using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Machines.Commands
{
    internal sealed class CleanFormCommand : CommandBase
    {
        private readonly MachinesListViewModel _viewModel;

        public CleanFormCommand(MachinesListViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.CleanForm();
        }
    }
}