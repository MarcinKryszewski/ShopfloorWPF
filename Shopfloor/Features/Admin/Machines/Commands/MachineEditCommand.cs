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

            Machine machine = new()
            {
                Id = (int)_viewModel.Id,
                Name = _viewModel.MachineName,
                Number = _viewModel.MachineNumber,
                SapNumber = _viewModel.SapNumber,
                ParentId = parentId,
                IsActive = selectedMachine.IsActive,
            };

            if (_viewModel.HasErrors) return;

            _ = _provider.Update(machine);
            _viewModel.ReloadData();
            _viewModel.CleanForm();
            _viewModel.UpdateList();
        }
    }
}