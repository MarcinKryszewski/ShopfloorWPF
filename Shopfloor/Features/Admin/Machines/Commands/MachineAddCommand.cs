using Shopfloor.Features.Admin.Machines.List;
using Shopfloor.Models.MachineModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Machines.Commands
{
    public class MachineAddCommand : CommandBase
    {
        private readonly MachinesListViewModel _viewModel;
        private readonly MachineProvider _provider;

        public MachineAddCommand(MachinesListViewModel viewModel, MachineProvider provider)
        {
            _viewModel = viewModel;
            _provider = provider;
        }

        public override void Execute(object? parameter)
        {
            int? parentId = _viewModel.SelectedParent?.Id;

            Machine machine = new(
                _viewModel.MachineName,
                _viewModel.MachineNumber,
                _viewModel.SapNumber,
                parentId,
                true);

            if (_viewModel.HasErrors) return;

            _ = _provider.Create(machine);
            _viewModel.ReloadData();
            _viewModel.CleanForm();
            _viewModel.AddToList(machine);
            //_viewModel.MachinesList.Refresh();
        }
    }
}