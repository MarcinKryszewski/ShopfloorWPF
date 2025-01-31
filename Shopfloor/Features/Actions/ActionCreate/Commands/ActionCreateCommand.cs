using System;
using System.Threading.Tasks;
using Shopfloor.Models.Activities;
using Shopfloor.Roots;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.ActionCreate.Commands
{
    internal class ActionCreateCommand : CommandBase
    {
        private readonly ActivitiesRoot _activitiesRoot;
        public ActionCreateCommand(ActivitiesRoot activitiesRoot)
        {
            _activitiesRoot = activitiesRoot;
        }
        public event EventHandler? ExecuteFinished;
        public string NotifyText { get; private set; } = string.Empty;
        public bool ExecutedSuccessful { get; private set; }
        public override void Execute(object? parameter)
        {
            if (parameter is null)
            {
                return;
            }
            ActivityCreation activity = (ActivityCreation)parameter;
            Task.Run(() => CreateActivity(activity));
        }
        protected void OnExecuteFinished(EventArgs e) => ExecuteFinished?.Invoke(this, e);
        private async Task CreateActivity(ActivityCreation activity)
        {
            const string errorExists = "Popraw błędy";
            const string actionCompletedSuccessfully = "Dodano działanie pomyślnie";
            ExecutedSuccessful = false;
            ActivityValidation validation = new();

            validation.Validate(activity);
            NotifyText = errorExists;
            if (!activity.HasErrors)
            {
                await _activitiesRoot.CreateActivity(activity);
                NotifyText = actionCompletedSuccessfully;
                ExecutedSuccessful = true;
            }
            OnExecuteFinished(EventArgs.Empty);
        }
    }
}