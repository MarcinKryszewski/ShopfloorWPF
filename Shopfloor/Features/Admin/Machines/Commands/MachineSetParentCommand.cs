using Shopfloor.Models.MachineModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Machines.Commands
{
    internal sealed class MachineSetParentCommand : CommandBase
    {
        private readonly MachinesListViewModel _viewModel;

        public MachineSetParentCommand(MachinesListViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is null) return;
            if (parameter is not Machine) return;
            Machine machine = (Machine)parameter;

            _viewModel.MachinesList.Filter = null;
            _viewModel.SelectedParent = machine;
        }
    }
}