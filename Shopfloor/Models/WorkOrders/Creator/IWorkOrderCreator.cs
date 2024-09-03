namespace Shopfloor.Models.WorkOrders.Creator
{
    public interface IWorkOrderCreator
    {
        public WorkOrderModel Create(WorkOrderDto workOrder);
    }
}