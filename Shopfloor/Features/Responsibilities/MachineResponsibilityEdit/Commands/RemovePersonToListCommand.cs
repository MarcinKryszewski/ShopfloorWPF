using System;
using System.Collections.Generic;
using Shopfloor.Models.Persons;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Responsibilities.MachineResponsibilityEdit.Commands
{
    internal class RemovePersonToListCommand : CommandBase
    {
        private readonly Notification _notificationError = new() { Message = "Nie usunięto osoby! Spróbuj ponownie!", Type = NotifierType.Error };
        private readonly Notification _notificationSuccess = new() { Message = "Usunięto osobę poprawnie!", Type = NotifierType.Success };
        private readonly List<Person> _persons;
        public RemovePersonToListCommand(List<Person> persons)
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
            _persons.Remove(person);
            OnDataChanged(_notificationSuccess);
        }
        protected void OnDataChanged(Notification e) => DataChanged?.Invoke(this, e);
    }
}