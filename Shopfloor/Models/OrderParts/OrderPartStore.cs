
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.OrderParts
{
    internal class OrderPartStore : IStore<OrderPartModel>
    {
        private readonly List<OrderPartModel> _data = [];
        public List<OrderPartModel> Data => _data;
        public Task AddItem(OrderPartModel item)
        {
            _data.Add(item);
            return Task.CompletedTask;
        }
        public async Task ReloadData()
        {
            await Task.Delay(0);
        }
    }
}