using System;
using Shopfloor.Models.Interfaces;

namespace Shopfloor.Models.WorkOrders.Creator
{
    public class WorkOrderCreator : IModelCreator<WorkOrderModel, WorkOrderDto>
    {
        public WorkOrderModel Create(WorkOrderDto workOrder)
        {
            Random random = new(); //save to Db, return Id
            return new WorkOrderModel()
            {
                Id = random.Next(),
                Description = workOrder.Description,
                Parts = workOrder.Parts,
                LineId = workOrder.LineId,
                Line = workOrder.Line,
            };
        }
    }
}