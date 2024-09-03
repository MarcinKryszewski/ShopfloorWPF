using System;

namespace Shopfloor.Models.WorkOrders.Creator
{
    public class WorkOrderCreator : IWorkOrderCreator
    {
        public WorkOrderModel Create(WorkOrderDto workOrder)
        {
            Random random = new(); //save to Db, return Id
            return new WorkOrderModel()
            {
                Id = random.Next(),
                Description = workOrder.Description,
                Parts = workOrder.Parts,
            };
        }
    }
}