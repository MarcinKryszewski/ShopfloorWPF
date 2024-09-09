using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Interfaces;
using Shopfloor.Models.WorkOrders;

namespace Shopfloor.UnitOfWorks
{
    internal class WorkOrdersListRoot : IUnitOfWork
    {
        private readonly WorkOrderStore _workOrderStore;
        private readonly WorkOrderRepository _workOrderRepository;

        public WorkOrdersListRoot(WorkOrderStore workOrderStore, WorkOrderRepository workOrderRepository)
        {
            _workOrderStore = workOrderStore;
            _workOrderRepository = workOrderRepository;
        }
        public async Task<IEnumerable<WorkOrderModel>> GetWorkOrders()
        {
            return await _workOrderStore.GetDataAsync();
        }
    }
}