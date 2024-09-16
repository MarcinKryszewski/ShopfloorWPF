using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.WorkOrders;

namespace Shopfloor.UnitOfWorks
{
    internal class WorkOrderCreateRoot : IUnitOfWork
    {
        private readonly IRepository<LineModel, LineCreationModel> _lineRepository;
        private readonly WorkOrderValidation _validation = new();
        private readonly IRepository<WorkOrderModel, WorkOrderCreationModel> _workOrderRepository;
        public WorkOrderCreateRoot(
            IRepository<WorkOrderModel, WorkOrderCreationModel> workOrderRepository,
            IRepository<LineModel, LineCreationModel> lineRepository)
        {
            _workOrderRepository = workOrderRepository;
            _lineRepository = lineRepository;
        }
        public event EventHandler? DecoratingCompleted;
        public async Task CreateWorkOrder(WorkOrderCreationModel data)
        {
            _validation.Validate(data);
            if (data.HasErrors)
            {
                string errorText = "Nie udało się utworzyć nowego działania. Popraw dane i spróbuj ponownie!";
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }

            await _workOrderRepository.Create(data);
        }
        public async Task<IEnumerable<LineModel>> GetLines() => await _lineRepository.GetDataAsync();
        public async Task<IEnumerable<WorkOrderModel>> GetWorkOrders() => await _workOrderRepository.GetDataAsync();
        protected void OnDecoratingCompleted(EventArgs e) => DecoratingCompleted?.Invoke(this, e);
    }
}