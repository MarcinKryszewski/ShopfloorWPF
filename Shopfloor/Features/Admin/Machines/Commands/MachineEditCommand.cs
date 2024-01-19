using Shopfloor.Features.Admin.Machines.List;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Machines.Commands
{
    public class MachineEditCommand : CommandBase
    {
        private readonly MachinesListViewModel _viewModel;
        private readonly MachineProvider _provider;

        public MachineEditCommand(MachinesListViewModel viewModel, MachineProvider provider)
        {
            _viewModel = viewModel;
            _provider = provider;
        }

        public override void Execute(object? parameter)
        {
            Machine? selectedMachine = _viewModel.SelectedMachine;
            if (selectedMachine == null) return;
            if (selectedMachine.Id == null) return;
            int? parentId = _viewModel.SelectedParent?.Id;



            Machine machine = new(
                (int)selectedMachine.Id,
                _viewModel.MachineName,
                _viewModel.MachineNumber,
                parentId,
                selectedMachine.IsActive
            );

            _ = _provider.Update(machine);
        }
    }
}