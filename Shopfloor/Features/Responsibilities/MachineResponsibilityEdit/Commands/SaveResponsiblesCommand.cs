using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Persons;
using Shopfloor.Roots;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.MachineResponsibilityEdit.Commands
{
    internal class SaveResponsiblesCommand : CommandBase
    {
        private readonly MachinesRoot _root;
        private readonly Machine _machine;
        private readonly List<Person> _persons;
        private readonly Notification _notificationError = new() { Message = "Błąd zapisu osób odpowiedzialnych! Spróbuj ponownie!", Type = NotifierType.Error };
        private readonly Notification _notificationSuccess = new() { Message = "Dodano osoby odpowiedzialne!", Type = NotifierType.Success };
        public SaveResponsiblesCommand(
            Machine selectedMachine,
            List<Person> selectedPersons,
            MachinesRoot machinesRoot)
        {
            _machine = selectedMachine;
            _persons = selectedPersons;
            _root = machinesRoot;
        }
        public event EventHandler<Notification>? DataChanged;
        public override void Execute(object? parameter)
        {
            Task.Run(() => _root.UpdateResponsibles(_machine, _persons)).ContinueWith(
                t =>
                {
                    if (t.IsCompletedSuccessfully)
                    {
                        OnSave(_notificationSuccess);
                        return;
                    }
                    OnSave(_notificationError);
                });
        }
        protected void OnSave(Notification e) => DataChanged?.Invoke(this, e);
    }
}