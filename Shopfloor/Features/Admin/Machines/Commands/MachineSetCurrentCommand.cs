using System.Collections.Generic;
using System.Linq;
using Shopfloor.Models.MachineModel;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Admin.Machines.Commands
{
    internal sealed class MachineSetCurrentCommand : CommandBase
    {
        private readonly MachinesListViewModel _viewModel;

        public MachineSetCurrentCommand(MachinesListViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is null)
            {
                return;
            }

            if (parameter is not Machine)
            {
                return;
            }

            Machine machine = (Machine)parameter;
            _viewModel.SelectedMachine = machine;

            //_viewModel.MachinesList.Filter = null;
            if (machine.ParentId == null)
            {
                _viewModel.MachinesList.Filter = null;
                _viewModel.SelectedParent = null;
                return;
            }
            IEnumerable<Machine> source = _viewModel.MachinesList.SourceCollection.OfType<Machine>();
            _viewModel.SelectedParent = source.FirstOrDefault(m => m.Id == machine.ParentId);
            //_viewModel.MachinesList.Filter = null;
        }
    }
}