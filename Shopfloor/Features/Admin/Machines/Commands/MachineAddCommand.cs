using Shopfloor.Features.Admin.Machines.List;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
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

            _ = _provider.Create(new Machine(
                _viewModel.MachineName,
                _viewModel.MachineNumber,
                parentId,
                true
            ));

            _ = _viewModel.UpdateMachines();
        }
    }
}