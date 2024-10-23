using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.Mechanic.WorkOrderAddNew;
using Shopfloor.Features.Mechanic.WorkOrderDetails;
using Shopfloor.Features.Mechanic.WorkOrderEdit;
using Shopfloor.Features.Plannist.WorkOrdersList.Commands;
using Shopfloor.Features.WorkInProgressFeature;
using Shopfloor.Models.WorkOrders;
using Shopfloor.Roots;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Plannist.WorkOrdersList
{
    internal class WorkOrdersListViewModel : ViewModelBase
    {
        private readonly WorkOrderContext _store;
        private readonly WorkOrdersListRoot _root;
        private readonly List<WorkOrderModel> _workOrders = [];

        public WorkOrdersListViewModel(WorkOrdersListRoot root, WorkOrderContext store, ViewModelBaseDependecies dependecies)
        : base(dependecies)
        {
            _root = root;
            _store = store;
            _root.DataChanged += DataChanged;

            _ = LoadDataAsync();

            WorkOrderCancelCommand = new WorkOrderCancelCommand(Notifier, root);
            WorkOrderCancelInfoCommand = new WorkOrderCancelInfoCommand(Notifier);
            WorkOrderConfirmCommand = new WorkInProgressCommand(Notifier); // TODO
            WorkOrderDetailsCommand = new NavigationCommand<WorkOrderDetailsViewModel>(NavigationService).Navigate();
            WorkOrderEditCommand = new NavigationCommand<WorkOrderEditViewModel>(NavigationService).Navigate();
            WorkOrderCreateCommand = new NavigationCommand<WorkOrderAddNewViewModel>(NavigationService).Navigate();
        }
        public WorkOrderModel? WorkOrder
        {
            get => _store.WorkOrder;
            set => _store.WorkOrder = value;
        }
        public ICommand WorkOrderCancelInfoCommand { get; }
        public ICommand WorkOrderCancelCommand { get; }
        public ICommand WorkOrderConfirmCommand { get; }
        public ICommand WorkOrderCreateCommand { get; }
        public ICommand WorkOrderDetailsCommand { get; }
        public ICommand WorkOrderEditCommand { get; }
        public ICollectionView WorkOrders => CollectionViewSource.GetDefaultView(_workOrders);
        public void DataChanged(object? sender, EventArgs e)
        {
            HandleWorkOrderRemoval(e);
            WorkOrders.Refresh();
        }
        private void HandleWorkOrderRemoval(EventArgs e)
        {
            if (e is ObjectEventArgs { Args: WorkOrderModel deletedWorkOrder })
            {
                _workOrders.Remove(deletedWorkOrder);
            }
        }
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            IEnumerable<WorkOrderModel> dataWorkOrder = await _root.GetWorkOrders();

            tasks.Add(BatchListUpdater.UpdateAsync(
                dataWorkOrder,
                _workOrders,
                WorkOrders));

            await Task.WhenAll(tasks);
        }
    }
}