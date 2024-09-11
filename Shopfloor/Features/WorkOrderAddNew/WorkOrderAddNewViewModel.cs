using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Features.WorkOrderAddNew.Commands;
using Shopfloor.Features.WorkOrdersList;
using Shopfloor.Models.Lines;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;
using Shopfloor.UnitOfWorks;

namespace Shopfloor.Features.WorkOrderAddNew
{
    internal class WorkOrderAddNewViewModel : ViewModelBase
    {
        private readonly WorkOrderCreateRoot _unitOfWork;
        private readonly List<LineModel> _lines = [];
        public WorkOrderAddNewViewModel(INavigationService navigationService, INotifier notifier, WorkOrderCreateRoot unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _ = LoadDataAsync();

            WorkOrdersListNavigate = new NavigationCommand<WorkOrdersListViewModel>(navigationService).Navigate();
            WorkOrderCreateCommand = new WorkOrderCreateCommand(notifier, _unitOfWork);
        }
        public ICollectionView Lines => CollectionViewSource.GetDefaultView(_lines);
        public WorkOrderDto WorkOrderDto { get; set; } = new();
        public ICommand WorkOrderCreateCommand { get; }
        public ICommand WorkOrdersListNavigate { get; }
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            IEnumerable<LineModel> dataWorkOrder = await _unitOfWork.GetLines();

            tasks.Add(BatchListUpdater.UpdateAsync(
                dataWorkOrder,
                _lines,
                Lines));

            await Task.WhenAll(tasks);
        }
    }
}