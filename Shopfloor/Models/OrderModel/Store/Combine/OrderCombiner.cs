using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.OrderModel.Store.Combine
{
    internal sealed class OrderCombiner : ICombinerManager<Order>
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}