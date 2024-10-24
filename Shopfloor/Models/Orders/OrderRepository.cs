using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Orders
{
    internal class OrderRepository : IRepository<OrderModel, OrderCreationModel>
    {
        public HashSet<Type> Merges => throw new NotImplementedException();

        public Task<OrderModel> Create(OrderCreationModel item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderModel>> GetDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task Update(OrderCreationModel item)
        {
            throw new NotImplementedException();
        }
    }
}