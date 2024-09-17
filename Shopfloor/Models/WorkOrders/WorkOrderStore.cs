using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.WorkOrders
{
    internal class WorkOrderStore : IStore<WorkOrderModel>
    {
        private readonly List<WorkOrderModel> _data = [];
        public List<WorkOrderModel> Data => _data;

        public Task AddItem(WorkOrderModel item)
        {
            _data.Add(item);
            return Task.CompletedTask;
        }
        public async Task ReloadData()
        {
        }
    }
}