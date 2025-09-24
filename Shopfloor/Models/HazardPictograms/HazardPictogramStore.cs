using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.HazardPictograms
{
    internal class HazardPictogramStore : IStore<HazardPictogram>
    {
        private readonly List<HazardPictogram> _data = [];
        public List<HazardPictogram> Data => _data;
        public Task AddItem(HazardPictogram item)
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