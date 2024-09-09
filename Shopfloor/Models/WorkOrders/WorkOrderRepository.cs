using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.WorkOrders
{
    internal class WorkOrderRepository : IRepository<WorkOrderModel>
    {
        public Task<WorkOrderModel> Create()
        {
            throw new System.NotImplementedException();
        }

        public Task<WorkOrderModel> Delete()
        {
            throw new System.NotImplementedException();
        }

        public Task<WorkOrderModel> Update()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<WorkOrderModel>> GetData()
        {
            List<WorkOrderModel> aaa = [
                new WorkOrderModel() { Id = 1, Description = "WorkOrder1", LineId = 1 },
                new WorkOrderModel() { Id = 2, Description = "WorkOrder2", LineId = 2 },
                new WorkOrderModel() { Id = 3, Description = "WorkOrder3", LineId = 3 },
                new WorkOrderModel() { Id = 4, Description = "WorkOrder4", LineId = 1 },
                new WorkOrderModel() { Id = 5, Description = "WorkOrder5", LineId = 4 },
                new WorkOrderModel() { Id = 6, Description = "WorkOrder6", LineId = 2 },
                new WorkOrderModel() { Id = 7, Description = "WorkOrder7", LineId = 5 },
                new WorkOrderModel() { Id = 8, Description = "WorkOrder8", LineId = 3 },
                new WorkOrderModel() { Id = 9, Description = "WorkOrder9", LineId = 4 },
                new WorkOrderModel() { Id = 10, Description = "WorkOrder10", LineId = 1 },
                new WorkOrderModel() { Id = 11, Description = "WorkOrder11", LineId = 2 },
                new WorkOrderModel() { Id = 12, Description = "WorkOrder12", LineId = 5 },
                new WorkOrderModel() { Id = 13, Description = "WorkOrder13", LineId = 3 },
                new WorkOrderModel() { Id = 14, Description = "WorkOrder14", LineId = 4 },
                new WorkOrderModel() { Id = 15, Description = "WorkOrder15", LineId = 1 },
                new WorkOrderModel() { Id = 16, Description = "WorkOrder16", LineId = 2 },
                new WorkOrderModel() { Id = 17, Description = "WorkOrder17", LineId = 3 },
                new WorkOrderModel() { Id = 18, Description = "WorkOrder18", LineId = 5 },
                new WorkOrderModel() { Id = 19, Description = "WorkOrder19", LineId = 4 },
                new WorkOrderModel() { Id = 20, Description = "WorkOrder20", LineId = 1 },
                new WorkOrderModel() { Id = 21, Description = "WorkOrder21", LineId = 2 },
                new WorkOrderModel() { Id = 22, Description = "WorkOrder22", LineId = 3 },
            ];

            await Task.Delay(1);
            return aaa;
        }
    }
}