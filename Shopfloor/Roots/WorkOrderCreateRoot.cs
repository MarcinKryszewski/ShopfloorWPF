using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Contexts.PartsBasket;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.WorkOrderParts;
using Shopfloor.Models.WorkOrders;

namespace Shopfloor.Roots
{
    internal class WorkOrderCreateRoot : IRoot
    {
        private readonly IRepository<LineModel, LineCreationModel> _lineRepository;
        private readonly IRepository<WorkOrderModel, WorkOrderCreationModel> _workOrderRepository;
        private readonly IRepository<WorkOrderPartModel, WorkOrderPartCreationModel> _workOrderPartRepository;
        private readonly PartsBasketContext _basket;
        private readonly WorkOrderValidation _validation = new();
        public WorkOrderCreateRoot(
            IRepository<WorkOrderModel, WorkOrderCreationModel> workOrderRepository,
            IRepository<LineModel, LineCreationModel> lineRepository,
            IRepository<WorkOrderPartModel, WorkOrderPartCreationModel> workOrderPartRepository,
            PartsBasketContext basket)
        {
            _workOrderRepository = workOrderRepository;
            _lineRepository = lineRepository;
            _basket = basket;
            _workOrderPartRepository = workOrderPartRepository;
        }
        public event EventHandler? DataChanged;
        public async Task<IEnumerable<LineModel>> GetLines() => await _lineRepository.GetDataAsync();
        public async Task CreateWorkOrder(WorkOrderCreationModel data)
        {
            _validation.Validate(data);
            if (data.HasErrors)
            {
                string errorText = "Nie udało się utworzyć nowego działania. Popraw dane i spróbuj ponownie!";
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }
            _basket.WorkOrder = await _workOrderRepository.Create(data);
            if (_basket.Parts.Any())
            {
                await CreateWorkOrderParts();
            }
        }
        public async Task<IEnumerable<WorkOrderModel>> GetWorkOrders() => await _workOrderRepository.GetDataAsync();
        protected void OnDecoratingCompleted(EventArgs e) => DataChanged?.Invoke(this, e);
        private async Task CreateWorkOrderParts()
        {
            List<Task> tasks = [];

            foreach (WorkOrderPartCreationModel part in _basket.Parts)
            {
                tasks.Add(_workOrderPartRepository.Create(part));
            }
            await Task.WhenAll(tasks);
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}