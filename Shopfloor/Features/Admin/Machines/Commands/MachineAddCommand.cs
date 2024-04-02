using Shopfloor.Models.MachineModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Machines.Commands
{
    internal sealed class MachineAddCommand : CommandBase
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

            Machine machine = new()
            {
                Name = _viewModel.MachineName,
                Number = _viewModel.MachineNumber,
                SapNumber = _viewModel.SapNumber,
                ParentId = parentId,
                IsActive = true,
            };

            if (!_viewModel.IsDataValidate) return;

            _ = _provider.Create(machine);
            _viewModel.ReloadData();
            _viewModel.CleanForm();
            _viewModel.AddToList(machine);
            //_viewModel.MachinesList.Refresh();
        }
    }
}