using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;
using Shopfloor.UnitOfWorks;

namespace Shopfloor.Features.WorkOrdersList
{
    internal class WorkOrdersListViewModel : ViewModelBase
    {
        private readonly WorkOrdersListRoot _unitOfWork;
        private readonly List<WorkOrderModel> _workOrders = [];
        public WorkOrdersListViewModel(WorkOrdersListRoot unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ = LoadDataAsync();
        }
        public ICollectionView WorkOrders => CollectionViewSource.GetDefaultView(_workOrders);
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            IEnumerable<WorkOrderModel> dataWorkOrder = await _unitOfWork.GetWorkOrders();

            tasks.Add(BatchListUpdater.UpdateAsync(
                dataWorkOrder,
                _workOrders,
                WorkOrders));

            await Task.WhenAll(tasks);
        }
    }
}