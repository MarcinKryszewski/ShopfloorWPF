using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.ActivitiesInstructions
{
    internal class ActivityInstructionStore : IStore<ActivityInstruction>
    {
        private readonly List<ActivityInstruction> _data = [];
        public List<ActivityInstruction> Data => _data;
        public Task AddItem(ActivityInstruction item)
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