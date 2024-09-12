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
        private readonly IStore<WorkOrderModel> _workOrderStore;
        private readonly IStore<LineModel> _lineStore;
        private readonly WorkOrderValidation _validation = new();
        private readonly IRepository<WorkOrderModel, WorkOrderCreationModel> _repository;
        public WorkOrderCreateRoot(IStore<WorkOrderModel> workOrderStore, IStore<LineModel> lineStore, IRepository<WorkOrderModel, WorkOrderCreationModel> repository)
        {
            _workOrderStore = workOrderStore;
            _lineStore = lineStore;
            _repository = repository;
        }
        public event EventHandler? DecoratingCompleted;
        public async Task<IEnumerable<WorkOrderModel>> GetWorkOrders() => await _workOrderStore.GetDataAsync();
        public async Task<IEnumerable<LineModel>> GetLines() => await _lineStore.GetDataAsync();
        public async Task CreateWorkOrder(WorkOrderCreationModel data)
        {
            _validation.Validate(data);
            if (data.HasErrors)
            {
                string errorText = "Nie udało się utworzyć nowego działania. Popraw dane i spróbuj ponownie!";
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }

            WorkOrderModel item = await _repository.Create(data);
            await _workOrderStore.AddItem(item);
        }
        protected void OnDecoratingCompleted(EventArgs e) => DecoratingCompleted?.Invoke(this, e);
    }
}