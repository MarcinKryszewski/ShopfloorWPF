using System.Collections.Generic;
using System.Linq;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.WorkOrderParts;

namespace Shopfloor.Models.Orders
{
    internal class OrderCreationModel : ModelValidationBase, IModelCreationModel<OrderModel>
    {
        public List<WorkOrderPartModel> Parts { get; set; } = [];
        public double TotalValue => Parts.Sum(x => x.TotalValue);
        public OrderModel CreateModel(int id)
        {
            return new OrderModel() { Id = id, Parts = Parts };
        }
    }
}