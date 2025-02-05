using System;
using System.Threading.Tasks;
using Shopfloor.Contexts;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Workshops;
using Shopfloor.Roots;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Actions.ActionTransfer.Commands
{
    internal class ActionTransferCommand : CommandBase
    {
        private readonly ActionTransferRoot _root;
        private readonly Activity? _activity;
        private readonly Notification _notificationError = new() { Message = "Nie przekazano działania! Spróbuj ponownie!", Type = NotifierType.Error };
        private readonly Notification _notificationSuccess = new() { Message = "Przekazano działanie!", Type = NotifierType.Success };
        private readonly Notification _notificationSameWorkshop = new() { Message = "Nie można przekazać do tego samego warsztatu!", Type = NotifierType.Warning };
        public ActionTransferCommand(
            ActionTransferRoot root,
            ActivityContext activityContext)
        {
            _root = root;
            _activity = activityContext.Activity;
        }
        public event EventHandler<Notification>? ExecuteFinished;
        public override void Execute(object? parameter)
        {
            if (parameter is not Workshop)
            {
                OnExecuteFinished(_notificationError);
                return;
            }

            Workshop workshop = (Workshop)parameter;

            if (workshop == (_activity?.Workshop ?? null))
            {
                OnExecuteFinished(_notificationSameWorkshop);
                return;
            }

            Task.Run(() => _root.TransferAction(_activity, workshop));
            OnExecuteFinished(_notificationSuccess);
        }
        protected void OnExecuteFinished(Notification e) => ExecuteFinished?.Invoke(this, e);
    }
}