using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Machines
{
    internal class MachineStore : IStore<Machine>
    {
        private readonly List<Machine> _data = [];
        public List<Machine> Data => _data;
        public Task AddItem(Machine item)
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