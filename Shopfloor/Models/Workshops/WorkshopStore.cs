using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Workshops
{
    internal class WorkshopStore : IStore<Workshop>
    {
        private readonly List<Workshop> _data = [];
        public List<Workshop> Data => _data;
        public Task AddItem(Workshop item)
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