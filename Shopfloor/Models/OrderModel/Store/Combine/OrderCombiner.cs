using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.OrderModel.Store.Combine
{
    internal sealed class OrderCombiner : ICombinerManager<Order>
    {
        public bool IsCombined { get; private set; }
        public Task CombineAll(bool shouldForce = false)
        {
            if (IsCombined && !shouldForce) return Task.CompletedTask;
            IsCombined = true;
            return Task.CompletedTask;
        }
        public Task CombineOne(Order item)
        {
            return Task.CompletedTask;
        }
    }
}