using System;
using System.Collections.Generic;
using Shopfloor.Models.Persons;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.MachineResponsibilityEdit.Commands
{
    internal class AddPersonToListCommand : CommandBase
    {
        private readonly List<Person> _persons;
        private readonly Notification _notificationError = new() { Message = "Nie dodano osoby! Spróbuj ponownie!", Type = NotifierType.Error };
        private readonly Notification _notificationSuccess = new() { Message = "Dodano osobę poprawnie!", Type = NotifierType.Success };
        public AddPersonToListCommand(List<Person> persons)
        {
            _persons = persons;
        }
        public event EventHandler<Notification>? DataChanged;
        public override void Execute(object? parameter)
        {
            if (parameter is not Person)
            {
                OnDataChanged(_notificationError);
                return;
            }
            Person person = (Person)parameter;
            _persons.Add(person);
            OnDataChanged(_notificationSuccess);
        }
        protected void OnDataChanged(Notification e) => DataChanged?.Invoke(this, e);
    }
}