using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Activities
{
    internal class ActivityStore : IStore<Activity>
    {
        private readonly List<Activity> _data = [];
        public List<Activity> Data => _data;
        public Task AddItem(Activity item)
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