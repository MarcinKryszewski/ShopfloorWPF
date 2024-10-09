using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.WorkOrderParts
{
    internal class WorkOrderPartStore : IStore<WorkOrderPartModel>
    {
        private readonly List<WorkOrderPartModel> _data = [];

        public List<WorkOrderPartModel> Data => _data;
        public Task AddItem(WorkOrderPartModel item)
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