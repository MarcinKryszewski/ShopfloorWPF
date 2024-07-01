using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.OrderModel
{
    internal sealed class OrderStore : StoreBase<Order>
    {
        public OrderStore(IProvider<Order> provider) : base(provider)
        {
        }
    }
}