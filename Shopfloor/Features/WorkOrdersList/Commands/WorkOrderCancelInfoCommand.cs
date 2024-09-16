using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.WorkOrdersList.Commands
{
    internal class WorkOrderCancelInfoCommand : CommandBase
    {
        private readonly INotifier _notifier;

        public WorkOrderCancelInfoCommand(INotifier notifier)
        {
            _notifier = notifier;
        }

        public override void Execute(object? parameter)
        {
            string warningText = "Jeżeli chcesz usunąć to zadanie przytrzymaj shift podczas kliknięcia";
            _notifier.ShowWarning(warningText);
        }
    }
}