using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Models.WorkOrders.Creator;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.God.Commands
{
    public class CreateWorkOrderCommand : CommandBase
    {
        private readonly Func<WorkOrderModel, Task> _addWorkOrder;
        private readonly WorkOrderCreator _workOrderCreator;
        public CreateWorkOrderCommand(Func<WorkOrderModel, Task> addWorkOrder)
        {
            _workOrderCreator = new WorkOrderCreator();
            _addWorkOrder = addWorkOrder;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is not WorkOrderDto)
            {
                // notify error
                return;
            }

            WorkOrderDto workOrder = (WorkOrderDto)parameter;
            WorkOrderModel workOrderNew = _workOrderCreator.Create(workOrder);
            _addWorkOrder(workOrderNew);
        }
    }
}