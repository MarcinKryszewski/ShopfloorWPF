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
        private readonly IRepository<LineModel, LineCreationModel> _lineRepository;
        private readonly IRepository<WorkOrderModel, WorkOrderCreationModel> _workOrderRepository;
        public WorkOrdersListRoot(IRepository<WorkOrderModel, WorkOrderCreationModel> workOrderRepository, IRepository<LineModel, LineCreationModel> lineRepository)
        {
            _workOrderRepository = workOrderRepository;
            _lineRepository = lineRepository;
        }
        public event EventHandler? DecoratingCompleted;
        public async Task<IEnumerable<WorkOrderModel>> GetWorkOrders()
        {
            if (!_workOrderRepository.Merges.Contains(typeof(LineModel)))
            {
                _ = DecorateWorkOders();
            }
            return (await _workOrderRepository.GetDataAsync()).Where(x => !x.IsDeleted);
        }
        protected void OnDecoratingCompleted(EventArgs e) => DecoratingCompleted?.Invoke(this, e);
        private async Task DecorateWorkOders()
        {
            Task<List<WorkOrderModel>> workOrdersTask = _workOrderRepository.GetDataAsync();
            Task<List<LineModel>> lineTask = _lineRepository.GetDataAsync();

            await Task.WhenAll(workOrdersTask, lineTask);

            List<WorkOrderModel> workOrders = workOrdersTask.Result;
            List<LineModel> lines = lineTask.Result;

            Dictionary<int, LineModel>? lineDictionary = lines.ToDictionary(line => line.Id);
            foreach (WorkOrderModel workOrder in workOrders)
            {
                //await Task.Delay(300);
                workOrder.Line = lineDictionary.TryGetValue(workOrder.LineId, out LineModel? line) ? line : null;
            }
            _workOrderRepository.Merges.Add(typeof(LineModel));
            OnDecoratingCompleted(EventArgs.Empty);
        }
    }
}