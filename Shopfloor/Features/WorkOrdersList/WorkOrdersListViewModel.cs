using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.WorkInProgressFeature;
using Shopfloor.Features.WorkOrderAddNew;
using Shopfloor.Features.WorkOrderEdit;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;
using Shopfloor.UnitOfWorks;

namespace Shopfloor.Features.WorkOrdersList
{
    internal class WorkOrdersListViewModel : ViewModelBase
    {
        private readonly WorkOrderContext _store;
        private readonly WorkOrdersListRoot _unitOfWork;
        private readonly List<WorkOrderModel> _workOrders = [];

        public WorkOrdersListViewModel(WorkOrdersListRoot unitOfWork, WorkOrderContext store, ViewModelBaseDependecies dependecies)
        : base(dependecies)
        {
            _unitOfWork = unitOfWork;
            _store = store;
            _unitOfWork.DecoratingCompleted += WorkOrdersDecorated;

            _ = LoadDataAsync();

            WorkOrderCancelCommand = new WorkInProgressCommand(Notifier);
            WorkOrderConfirmCommand = new WorkInProgressCommand(Notifier);
            WorkOrderDetailsCommand = new WorkInProgressCommand(Notifier);
            WorkOrderEditCommand = new NavigationCommand<WorkOrderEditViewModel>(NavigationService).Navigate();
            WorkOrderCreateCommand = new NavigationCommand<WorkOrderAddNewViewModel>(NavigationService).Navigate();
        }
        public WorkOrderModel? WorkOrder
        {
            get => _store.WorkOrder;
            set => _store.WorkOrder = value;
        }
        public ICommand WorkOrderCancelCommand { get; }
        public ICommand WorkOrderConfirmCommand { get; }
        public ICommand WorkOrderCreateCommand { get; }
        public ICommand WorkOrderDetailsCommand { get; }
        public ICommand WorkOrderEditCommand { get; }
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