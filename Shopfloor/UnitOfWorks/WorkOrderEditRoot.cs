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
        private readonly IRepository<LineModel, LineCreationModel> _lineStore;
        private readonly IRepository<WorkOrderModel, WorkOrderCreationModel> _repository;
        private readonly WorkOrderValidation _validation = new();
        public WorkOrderEditRoot(IRepository<LineModel, LineCreationModel> lineRepository, IRepository<WorkOrderModel, WorkOrderCreationModel> workOrderRepository)
        {
            _lineStore = lineRepository;
            _repository = workOrderRepository;
        }
        public event EventHandler? DecoratingCompleted;
        public async Task EditWorkOrder(WorkOrderCreationModel data)
        {
            _validation.Validate(data);
            if (data.HasErrors)
            {
                string errorText = "Nie udało się utworzyć nowego działania. Popraw dane i spróbuj ponownie!";
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }
            await _repository.Update();
        }
        public async Task<IEnumerable<LineModel>> GetLines() => await _lineStore.GetDataAsync();
    }
}