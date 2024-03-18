using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.OrderModel
{
    internal sealed class OrderStore : IDataStore<Order>
    {
        private readonly IServiceProvider _databaseServices;
        private List<Order> _data = [];
        public OrderStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public List<Order> Data => _data;
        public bool IsLoaded { get; private set; }
        public Task Load()
        {
            OrderProvider provider = _databaseServices.GetRequiredService<OrderProvider>();
            _data = new(provider.GetAll().Result);
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            OrderProvider provider = _databaseServices.GetRequiredService<OrderProvider>();
            _data = new(await provider.GetAll());
        }
        public async Task CombineData()
        {
            List<Task> tasks = [];



            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
    }
}