using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Parts
{
    internal class PartStore : IStore<PartModel>
    {
        private readonly List<PartModel> _data = [];
        public List<PartModel> Data => _data;
        public Task AddItem(PartModel item)
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