using System.Collections.Generic;
using System.Linq;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.OrderStatuses;
using Shopfloor.Models.WorkOrderParts;

namespace Shopfloor.Models.Orders
{
    internal class OrderModel : IModel
    {
        required public int Id { get; init; }
        public List<WorkOrderPartModel> Parts { get; init; } = [];
        public List<OrderStatusModel> Statuses { get; init; } = [];
        public OrderStatusModel? LastStatus => Statuses.MaxBy(x => x.CreationDate);
        public double TotalValue => Parts.Sum(x => x.TotalValue);
    }
}