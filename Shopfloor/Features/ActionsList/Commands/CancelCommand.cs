
using Shopfloor.Contexts;
using Shopfloor.Models.Activities;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.ActionsList.Commands
{
    internal class CancelCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            if (parameter is null)
            {
                return;
            }
            Activity activity = (Activity)parameter;
            activity.Status = ActivityStatus.Canceled;
        }
    }
}