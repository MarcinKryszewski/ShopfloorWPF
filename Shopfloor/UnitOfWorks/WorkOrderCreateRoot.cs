using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.WorkOrders;

namespace Shopfloor.UnitOfWorks
{
    internal class WorkOrderCreateRoot : IUnitOfWork
    {
        private readonly IStore<WorkOrderModel> _workOrderStore;
        private readonly IStore<LineModel> _lineStore;
        public WorkOrderCreateRoot(IStore<WorkOrderModel> workOrderStore, IStore<LineModel> lineStore)
        {
            _workOrderStore = workOrderStore;
            _lineStore = lineStore;
        }
        public event EventHandler? DecoratingCompleted;
        protected void OnDecoratingCompleted(EventArgs e) => DecoratingCompleted?.Invoke(this, e);
        public async Task<IEnumerable<WorkOrderModel>> GetWorkOrders()
        {
            return await _workOrderStore.GetDataAsync();
        }
        public async Task<IEnumerable<LineModel>> GetLines()
        {
            return await _lineStore.GetDataAsync();
        }
        public async Task CreateWorkOrder(WorkOrderDto dto)
        {
            await Task.Delay(0);
        }
    }
}