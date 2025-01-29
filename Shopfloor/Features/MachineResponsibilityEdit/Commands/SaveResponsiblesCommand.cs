using System.Collections.Generic;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Persons;
using Shopfloor.Roots;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.MachineResponsibilityEdit.Commands
{
    internal class SaveResponsiblesCommand : CommandBase
    {
        private readonly MachinesRoot _machinesRoot;
        private readonly Machine _selectedMachine;
        private readonly List<Person> _selectedPersons;
        public SaveResponsiblesCommand(Machine selectedMachine, List<Person> selectedPersons, MachinesRoot machinesRoot)
        {
            _selectedMachine = selectedMachine;
            _selectedPersons = selectedPersons;
            _machinesRoot = machinesRoot;
        }

        public override void Execute(object? parameter)
        {
        }
    }
}