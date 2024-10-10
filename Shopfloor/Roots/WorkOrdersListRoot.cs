using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Parts;
using Shopfloor.Models.PartTypes;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Shared.HelperFunctions;

namespace Shopfloor.Roots
{
    internal class WorkOrdersListRoot : IRoot
    {
        private readonly IRepository<PartTypeModel, PartTypeCreationModel> _partTypesRepository;
        private readonly IRepository<PartModel, PartCreationModel> _partRepository;
        private readonly IRepository<LineModel, LineCreationModel> _lineRepository;
        private readonly IRepository<WorkOrderModel, WorkOrderCreationModel> _workOrderRepository;
        public WorkOrdersListRoot(
            IRepository<PartTypeModel, PartTypeCreationModel> partTypesRepository,
            IRepository<PartModel, PartCreationModel> partRepository,
            IRepository<WorkOrderModel, WorkOrderCreationModel> workOrderRepository,
            IRepository<LineModel, LineCreationModel> lineRepository)
        {
            _partTypesRepository = partTypesRepository;
            _partRepository = partRepository;
            _workOrderRepository = workOrderRepository;
            _lineRepository = lineRepository;
        }
        public event EventHandler? DataChanged;
        public async Task<IEnumerable<WorkOrderModel>> GetWorkOrders()
        {
            List<Task> tasks = [];
            if (!_workOrderRepository.Merges.Contains(typeof(LineModel)))
            {
                tasks.Add(DecorateWorkOders());
            }
            if (!_partRepository.Merges.Contains(typeof(PartTypeModel)))
            {
                tasks.Add(DecorateWithTypes());
            }
            await Task.WhenAll(tasks);
            OnDataChanged(EventArgs.Empty);

            return await _workOrderRepository.GetDataAsync();
        }
        public async Task CancelWorkOrder(WorkOrderModel workOrder)
        {
            await _workOrderRepository.Delete(workOrder.Id);
            OnDataChanged(new ObjectEventArgs(workOrder));
        }
        protected void OnDataChanged(EventArgs e) => DataChanged?.Invoke(this, e);
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
                workOrder.Line = lineDictionary.TryGetValue(workOrder.LineId, out LineModel? line) ? line : null;
            }
            _workOrderRepository.Merges.Add(typeof(LineModel));
        }
        private async Task DecorateWithTypes()
        {
            Task<List<PartModel>> partsTask = _partRepository.GetDataAsync();
            Task<List<PartTypeModel>> typeTask = _partTypesRepository.GetDataAsync();

            await Task.WhenAll(partsTask, typeTask);

            List<PartModel> parts = partsTask.Result;
            List<PartTypeModel> types = typeTask.Result;

            Dictionary<int, PartTypeModel>? typeDictionary = types.ToDictionary(line => line.Id);
            foreach (PartModel part in parts)
            {
                part.PartType = typeDictionary.TryGetValue(part.PartTypeId, out PartTypeModel? type) ? type : null;
            }
            _partRepository.Merges.Add(typeof(PartTypeModel));
        }
    }
}