using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.OrderModel.Store.Combine;

namespace Shopfloor.Models.OrderModel
{
    internal sealed class OrderStore : StoreBase<Order>
    {
        public OrderStore(OrderProvider provider, OrderCombiner combiner) : base(provider, combiner)
        {

        }
    }
}