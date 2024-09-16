using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.WorkOrders;

namespace Shopfloor.UnitOfWorks
{
    internal class WorkOrderEditRoot : IUnitOfWork
    {
        private readonly IRepository<LineModel, LineCreationModel> _lineRepository;
        private readonly IRepository<WorkOrderModel, WorkOrderCreationModel> _workOrderRepository;
        private readonly WorkOrderValidation _validation = new();
        public WorkOrderEditRoot(
            IRepository<LineModel, LineCreationModel> lineRepository,
            IRepository<WorkOrderModel, WorkOrderCreationModel> workOrderRepository)
        {
            _lineRepository = lineRepository;
            _workOrderRepository = workOrderRepository;
        }
        public event EventHandler? DecoratingCompleted;
        public async Task EditWorkOrder(WorkOrderCreationModel data)
        {
            string errorText = "Nie udało się zmienić działania. Popraw dane i spróbuj ponownie!";
            _validation.Validate(data);
            if (data.HasErrors)
            {
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }
            try
            {
                await _workOrderRepository.Update(data);
            }
            catch (Exception)
            {
                await Task.FromException(new InvalidOperationException(errorText));
            }
        }
        public async Task<IEnumerable<LineModel>> GetLines() => await _lineRepository.GetDataAsync();
    }
}