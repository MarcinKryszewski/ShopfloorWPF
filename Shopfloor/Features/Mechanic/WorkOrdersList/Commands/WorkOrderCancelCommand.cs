using System;
using System.Threading.Tasks;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Roots;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Mechanic.WorkOrdersList.Commands
{
    internal class WorkOrderCancelCommand : CommandBase
    {
        private readonly INotifier _notifier;
        private readonly WorkOrdersListRoot _root;

        public WorkOrderCancelCommand(INotifier notifier, WorkOrdersListRoot root)
        {
            _notifier = notifier;
            _root = root;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is not WorkOrderModel)
            {
                string errorText = "Nie udało się anulować tego działania";
                _notifier.ShowError(errorText);
                return;
            }

            string successText = "Anulowano zadanie";

            WorkOrderModel workOrder = (WorkOrderModel)parameter;
            _root.CancelWorkOrder(workOrder).Wait();
            _notifier.ShowSuccess(successText);
        }
    }
}