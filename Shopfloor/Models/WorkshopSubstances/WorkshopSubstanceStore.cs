using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.WorkshopSubstances
{
    internal class WorkshopSubstanceStore : IStore<WorkshopSubstance>
    {
        private readonly List<WorkshopSubstance> _data = [];
        public List<WorkshopSubstance> Data => _data;
        public Task AddItem(WorkshopSubstance item)
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