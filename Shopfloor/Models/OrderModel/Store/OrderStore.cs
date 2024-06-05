using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.OrderModel
{
    internal sealed class OrderStore : StoreBase<Order>
    {
        public OrderStore(OrderProvider provider) : base(provider)
        {
        }
    }
}