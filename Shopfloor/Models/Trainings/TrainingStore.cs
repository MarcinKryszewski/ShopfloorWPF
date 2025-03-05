using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Trainings
{
    internal class TrainingStore : IStore<Training>
    {
        private readonly List<Training> _data = [];
        public List<Training> Data => _data;
        public Task AddItem(Training item)
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