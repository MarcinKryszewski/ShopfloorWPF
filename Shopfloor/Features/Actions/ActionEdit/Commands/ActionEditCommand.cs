using System;
using System.Threading.Tasks;
using Shopfloor.Models.Activities;
using Shopfloor.Roots;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Actions.ActionEdit.Commands
{
    internal class ActionEditCommand : CommandBase
    {
        private readonly Notification _notificationError = new() { Message = "Popraw błędy", Type = NotifierType.Error };
        private readonly Notification _notificationSuccess = new() { Message = "Zmienieono działanie pomyślnie", Type = NotifierType.Success };
        private readonly ActivitiesRoot _activitiesRoot;
        public ActionEditCommand(ActivitiesRoot activitiesRoot)
        {
            _activitiesRoot = activitiesRoot;
        }
        public event EventHandler<Notification?>? ExecuteFinished;
        public override void Execute(object? parameter)
        {
            if (parameter is null)
            {
                return;
            }
            ActivityCreation activity = (ActivityCreation)parameter;
            Task.Run(() => EditeActivity(activity));
        }
        protected void OnExecuteFinished(Notification? e) => ExecuteFinished?.Invoke(this, e);
        private async Task EditeActivity(ActivityCreation activity)
        {
            ActivityValidation validation = new();

            validation.Validate(activity);
            if (activity.HasErrors)
            {
                OnExecuteFinished(_notificationError);
                return;
            }

            await _activitiesRoot.UpdateActivity(activity);
            OnExecuteFinished(_notificationSuccess);
        }
    }
}