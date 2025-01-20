
using System.Threading.Tasks;
using Shopfloor.Contexts;
using Shopfloor.Models.Activities;
using Shopfloor.Roots;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.ActionsList.Commands
{
    internal class CancelCommand : CommandBase
    {
        private readonly ActivitiesRoot _root;
        public CancelCommand(ActivitiesRoot root)
        {
            _root = root;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is null)
            {
                return;
            }
            Activity activity = (Activity)parameter;
            _root.DeleteActivity(activity).Wait();
        }
    }
}