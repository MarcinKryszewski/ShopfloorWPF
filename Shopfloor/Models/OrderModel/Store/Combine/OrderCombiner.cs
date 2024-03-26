using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.OrderModel.Store.Combine
{
    internal sealed class OrderCombiner : ICombiner<Order>
    {
        public Task Combine(List<Order> data)
        {
            return Task.CompletedTask;
        }
    }
}