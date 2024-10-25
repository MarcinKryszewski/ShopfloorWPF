using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.OrderParts
{
    internal class OrderPartProvider : IProvider<OrderPartModel, OrderPartCreationModel>
    {
        public Task<int> Create(OrderPartCreationModel item)
        {
            Random rnd = new();
            int id = rnd.Next(100, 100000);
            return Task.FromResult(id);
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<OrderPartModel>> GetAll()
        {
            IEnumerable<OrderPartModel> result = [];
            return Task.FromResult(result);
        }
        public Task<OrderPartModel> GetById(int id)
        {
            return Task.FromResult(new OrderPartModel() { Id = id, OrderId = 3453, PartId = 32423 });
        }
        public Task Update(OrderPartModel item)
        {
            return Task.CompletedTask;
        }
    }
}