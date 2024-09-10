using System;
using Shopfloor.Models.Interfaces;

namespace Shopfloor.Models.WorkOrders.Creators
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
                PartsId = workOrder.PartsId,
                Line = workOrder.Line,
                LineId = workOrder.LineId,
                CreateDate = DateTime.Now,
            };
        }
    }
}