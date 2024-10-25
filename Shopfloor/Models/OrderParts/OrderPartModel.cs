using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Orders;
using Shopfloor.Models.WorkOrderParts;

namespace Shopfloor.Models.OrderParts
{
    internal class OrderPartModel : IModel
    {
        required public int Id { get; init; }
        required public int OrderId { get; init; }
        required public int PartId { get; init; }
        public OrderModel? Order { get; set; }
        public WorkOrderPartModel? Part { get; set; }
        public void SetValues(OrderPartCreationModel data)
        {
        }
    }
}