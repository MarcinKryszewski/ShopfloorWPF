using Shopfloor.Features.Admin.Machines.List;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Machines.Commands
{
    public class CleanFormCommand : CommandBase
    {
        private readonly MachinesListViewModel _viewModel;

        public CleanFormCommand(MachinesListViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.IsEdit = !_viewModel.IsEdit;
        }
    }
}