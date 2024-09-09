using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Interfaces;

namespace Shopfloor.Models.WorkOrders
{
    internal class WorkOrderStore : IStore<WorkOrderModel>
    {
        private readonly WorkOrderRepository _repository;
        private List<WorkOrderModel> _workOrders = [];
        private bool _dataLoaded = false;
        public WorkOrderStore(WorkOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<WorkOrderModel>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                _workOrders = await LoadData();
            }
            return _workOrders;
        }

        public Task ReloadData()
        {
            List<WorkOrderModel> aaa = [
                new WorkOrderModel() { Id = 1, Description = "WorkOrder Alpha", LineId = 1 },
                new WorkOrderModel() { Id = 2, Description = "WorkOrder Beta", LineId = 3 },
                new WorkOrderModel() { Id = 3, Description = "WorkOrder Gamma", LineId = 2 },
                new WorkOrderModel() { Id = 4, Description = "WorkOrder Delta", LineId = 5 },
                new WorkOrderModel() { Id = 5, Description = "WorkOrder Epsilon", LineId = 4 },
                new WorkOrderModel() { Id = 6, Description = "WorkOrder Zeta", LineId = 1 },
                new WorkOrderModel() { Id = 7, Description = "WorkOrder Eta", LineId = 5 },
                new WorkOrderModel() { Id = 8, Description = "WorkOrder Theta", LineId = 4 },
                new WorkOrderModel() { Id = 9, Description = "WorkOrder Iota", LineId = 3 },
                new WorkOrderModel() { Id = 10, Description = "WorkOrder Kappa", LineId = 2 },
                new WorkOrderModel() { Id = 11, Description = "WorkOrder Lambda", LineId = 1 },
                new WorkOrderModel() { Id = 12, Description = "WorkOrder Mu", LineId = 5 },
                new WorkOrderModel() { Id = 13, Description = "WorkOrder Nu", LineId = 2 },
                new WorkOrderModel() { Id = 14, Description = "WorkOrder Xi", LineId = 3 },
                new WorkOrderModel() { Id = 15, Description = "WorkOrder Omicron", LineId = 4 },
                new WorkOrderModel() { Id = 16, Description = "WorkOrder Pi", LineId = 2 },
                new WorkOrderModel() { Id = 17, Description = "WorkOrder Rho", LineId = 5 },
                new WorkOrderModel() { Id = 18, Description = "WorkOrder Sigma", LineId = 3 },
                new WorkOrderModel() { Id = 19, Description = "WorkOrder Tau", LineId = 1 },
                new WorkOrderModel() { Id = 20, Description = "WorkOrder Upsilon", LineId = 4 },
                new WorkOrderModel() { Id = 21, Description = "WorkOrder Phi", LineId = 5 },
                new WorkOrderModel() { Id = 22, Description = "WorkOrder Chi", LineId = 2 },
            ];

            _workOrders.Clear();
            _workOrders.AddRange(aaa);

            return Task.CompletedTask;
        }
        private async Task<List<WorkOrderModel>> LoadData()
        {
            List<WorkOrderModel> data = await _repository.GetData();
            _dataLoaded = true;
            return data;
        }
    }
}