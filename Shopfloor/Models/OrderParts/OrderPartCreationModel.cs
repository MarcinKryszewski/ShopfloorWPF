using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Orders;
using Shopfloor.Models.WorkOrderParts;

namespace Shopfloor.Models.OrderParts
{
    internal class OrderPartCreationModel : ModelValidationBase, IModelCreationModel<OrderPartModel>
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PartId { get; set; }
        public OrderModel? Order { get; set; }
        public WorkOrderPartModel? Part { get; set; }
        public OrderPartModel CreateModel(int id)
        {
            return new OrderPartModel() { Id = id, OrderId = OrderId, PartId = PartId };
        }
    }
}