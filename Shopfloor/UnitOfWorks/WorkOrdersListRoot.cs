using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.WorkOrders;

namespace Shopfloor.UnitOfWorks
{
    internal class WorkOrdersListRoot : IUnitOfWork
    {
        private readonly IStore<WorkOrderModel> _workOrderStore;
        private readonly IStore<LineModel> _lineStore;
        public WorkOrdersListRoot(IStore<WorkOrderModel> workOrderStore, IStore<LineModel> lineStore)
        {
            _workOrderStore = workOrderStore;
            _lineStore = lineStore;
        }
        public event EventHandler? DecoratingCompleted;
        public async Task<IEnumerable<WorkOrderModel>> GetWorkOrders()
        {
            if (!_workOrderStore.Merges.Contains(typeof(LineModel)))
            {
                _ = DecorateWorkOders();
            }
            return await _workOrderStore.GetDataAsync();
        }
        protected void OnDecoratingCompleted(EventArgs e) => DecoratingCompleted?.Invoke(this, e);
        private async Task DecorateWorkOders()
        {
            Task<List<WorkOrderModel>> workOrdersTask = _workOrderStore.GetDataAsync();
            Task<List<LineModel>> lineTask = _lineStore.GetDataAsync();

            await Task.WhenAll(workOrdersTask, lineTask);

            List<WorkOrderModel> workOrders = workOrdersTask.Result;
            List<LineModel> lines = lineTask.Result;

            Dictionary<int, LineModel>? lineDictionary = lines.ToDictionary(line => line.Id);
            foreach (WorkOrderModel workOrder in workOrders)
            {
                workOrder.Line = lineDictionary.TryGetValue(workOrder.LineId, out LineModel? line) ? line : null;
            }
            _workOrderStore.Merges.Add(typeof(LineModel));
            OnDecoratingCompleted(EventArgs.Empty);
        }
    }
}