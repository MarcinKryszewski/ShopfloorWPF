using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Features.WorkInProgressFeature;
using Shopfloor.Features.WorkOrderAddNew;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;
using Shopfloor.UnitOfWorks;

namespace Shopfloor.Features.WorkOrdersList
{
    internal class WorkOrdersListViewModel : ViewModelBase
    {
        private readonly WorkOrdersListRoot _unitOfWork;
        private readonly List<WorkOrderModel> _workOrders = [];

        public WorkOrdersListViewModel(WorkOrdersListRoot unitOfWork, INotifier notification, INavigationService navigationService)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.DecoratingCompleted += WorkOrdersDecorated;

            _ = LoadDataAsync();

            WorkOrderCancelCommand = new WorkInProgressCommand(notification);
            WorkOrderConfirmCommand = new WorkInProgressCommand(notification);
            WorkOrderDetailsCommand = new WorkInProgressCommand(notification);
            WorkOrderEditCommand = new WorkInProgressCommand(notification);
            WorkOrderCreateCommand = new NavigationCommand<WorkOrderAddNewViewModel>(navigationService).Navigate();
        }

        public ICommand WorkOrderConfirmCommand { get; }
        public ICommand WorkOrderDetailsCommand { get; }
        public ICommand WorkOrderEditCommand { get; }
        public ICommand WorkOrderCancelCommand { get; }
        public ICommand WorkOrderCreateCommand { get; }

        public ICollectionView WorkOrders => CollectionViewSource.GetDefaultView(_workOrders);
        public void WorkOrdersDecorated(object? sender, EventArgs e)
        {
            WorkOrders.Refresh();
        }
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