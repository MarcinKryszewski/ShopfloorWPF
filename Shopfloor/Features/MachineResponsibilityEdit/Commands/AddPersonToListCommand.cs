using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using Shopfloor.Models.Persons;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.MachineResponsibilityEdit.Commands
{
    internal class AddPersonToListCommand : CommandBase
    {
        private readonly List<Person> _persons;
        public AddPersonToListCommand(List<Person> persons)
        {
            _persons = persons;
        }
        public event EventHandler? DataChanged;
        public override void Execute(object? parameter)
        {
            if (parameter is not Person)
            {
                return;
            }
            Person person = (Person)parameter;
            _persons.Add(person);
            OnDataChanged(EventArgs.Empty);
        }
        protected void OnDataChanged(EventArgs e) => DataChanged?.Invoke(this, e);
    }
}