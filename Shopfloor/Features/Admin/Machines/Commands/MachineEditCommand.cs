using Shopfloor.Features.Admin.Machines.List;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;
using System;

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
            if (_viewModel.SelectedMachine == null) return;
            int? parentId = _viewModel.SelectedParent?.Id;

            _ = _provider.Update(new Machine(
                _viewModel.SelectedMachine.Id,
                _viewModel.MachineName,
                _viewModel.MachineNumber,
                parentId
            ));

            _ = _viewModel.UpdateMachines();
        }
    }
}