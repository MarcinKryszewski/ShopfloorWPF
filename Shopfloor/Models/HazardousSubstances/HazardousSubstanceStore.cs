using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.HazardousSubstances
{
    internal class HazardousSubstanceStore : IStore<HazardousSubstance>
    {
        private readonly List<HazardousSubstance> _data = [];
        public List<HazardousSubstance> Data => _data;
        public Task AddItem(HazardousSubstance item)
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