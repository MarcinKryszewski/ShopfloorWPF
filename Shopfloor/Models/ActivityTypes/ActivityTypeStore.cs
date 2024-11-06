using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.ActivityTypes
{
    internal class ActivityTypeStore : IStore<ActivityType>
    {
        private readonly List<ActivityType> _data = [];
        public List<ActivityType> Data => _data;
        public Task AddItem(ActivityType item)
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