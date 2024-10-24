using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Orders
{
    internal class OrderProvider : IProvider<OrderModel, OrderCreationModel>
    {
        public Task<int> Create(OrderCreationModel item)
        {
            return Task.FromResult(0);
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<OrderModel>> GetAll()
        {
            IEnumerable<OrderModel> result = [];
            return Task.FromResult(result);
        }
        public Task<OrderModel> GetById(int id)
        {
            return Task.FromResult(new OrderModel() { Id = id });
        }
        public Task Update(OrderModel item)
        {
            return Task.CompletedTask;
        }
    }
}