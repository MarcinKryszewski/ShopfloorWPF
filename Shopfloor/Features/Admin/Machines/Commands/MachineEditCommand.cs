using Shopfloor.Features.Admin.Machines.List;
using Shopfloor.Models.MachineModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Machines.Commands
{
    internal sealed class MachineEditCommand : CommandBase
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
            if (_viewModel.Id == null) return;
            int? parentId = _viewModel.SelectedParent?.Id;

            Machine machine = new(
                (int)_viewModel.Id,
                _viewModel.MachineName,
                _viewModel.MachineNumber,
                _viewModel.SapNumber,
                parentId,
                selectedMachine.IsActive
            );

            if (_viewModel.HasErrors) return;

            _ = _provider.UpdateAmount(machine);
            _viewModel.ReloadData();
            _viewModel.CleanForm();
            _viewModel.UpdateList();
        }
    }
}