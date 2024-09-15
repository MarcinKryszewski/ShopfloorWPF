using Shopfloor.Models.WorkOrders;

namespace Shopfloor.Contexts
{
    internal class WorkOrderContext
    {
        public WorkOrderModel? WorkOrder { get; set; }
        public WorkOrderCreationModel ToWorkOrderCreation()
        {
            if (WorkOrder == null)
            {
                return new WorkOrderCreationModel();
            }

            WorkOrderCreationModel workOrderCreation = new()
            {
                Id = WorkOrder.Id,
                Description = WorkOrder.Description,
                Line = WorkOrder.Line,
                LineId = WorkOrder.LineId,
            };
            workOrderCreation.Parts.AddRange(WorkOrder.Parts);
            workOrderCreation.PartsId.AddRange(WorkOrder.PartsId);

            return workOrderCreation;
        }
    }
}