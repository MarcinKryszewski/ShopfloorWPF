using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Shopfloor.Features.Admin.Machines.List;
using Shopfloor.Models;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Machines.Commands
{
    public class MachineSetCurrentCommand : CommandBase
    {
        private readonly MachinesListViewModel _viewModel;

        public MachineSetCurrentCommand(MachinesListViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is null) return;
            if (parameter is not Machine) return;
            Machine machine = (Machine)parameter;
            _viewModel.SelectedMachine = machine;

            if (machine.ParentId == null)
            {
                _viewModel.SelectedParent = null;
                return;
            }
            IEnumerable<Machine> source = _viewModel.MachinesList.SourceCollection.OfType<Machine>();
            _viewModel.SelectedParent = source.FirstOrDefault(m => m.Id == machine.ParentId);
        }
    }
}