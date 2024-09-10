using System;
using System.Threading.Tasks;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Models.WorkOrders.Creators;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.God.Commands
{
    internal class CreateWorkOrderCommand : CommandBase
    {
        private readonly Func<WorkOrderModel, Task> _addWorkOrder;
        private readonly INotifier _notifier;
        private readonly WorkOrderCreator _workOrderCreator;
        public CreateWorkOrderCommand(Func<WorkOrderModel, Task> addWorkOrder, INotifier notifier)
        {
            _workOrderCreator = new WorkOrderCreator();
            _addWorkOrder = addWorkOrder;
            _notifier = notifier;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is not WorkOrderDto)
            {
                _notifier.ShowError("PROBLEM");
                return;
            }

            WorkOrderDto workOrder = (WorkOrderDto)parameter;
            WorkOrderModel workOrderNew = _workOrderCreator.Create(workOrder);
            _addWorkOrder(workOrderNew);
            _notifier.ShowSuccess("UTWORZONO NOWE DZIAŁANIE");
        }
    }
}