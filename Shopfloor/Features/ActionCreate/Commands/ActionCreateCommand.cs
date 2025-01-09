using System;
using System.Collections.Generic;
using System.Linq;
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

        public override void Execute(object? parameter)
        {
            if (parameter is null)
            {
                return;
            }
            ActivityCreation activity = (ActivityCreation)parameter;
            Task.Run(() => CreateActivity(activity));
        }
        private async Task CreateActivity(ActivityCreation activity)
        {
            ActivityValidation validation = new();
            validation.Validate(activity);
            if (!activity.HasErrors)
            {
                await _activitiesRoot.CreateActivity(activity);
            }
        }
    }
}