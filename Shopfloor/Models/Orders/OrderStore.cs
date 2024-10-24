using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Orders
{
    internal class OrderStore : IStore<OrderModel>
    {
        public List<OrderModel> Data => throw new NotImplementedException();

        public Task AddItem(OrderModel item)
        {
            throw new NotImplementedException();
        }

        public Task ReloadData()
        {
            throw new NotImplementedException();
        }
    }
}