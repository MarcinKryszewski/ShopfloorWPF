using System;
using System.Windows.Input;
using Shopfloor.Features.Mechanic.WorkOrdersList;
using Shopfloor.Features.PartsList;
using Shopfloor.Features.Plannist.OrdersList;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Layout.SidePanel
{
    internal sealed partial class SidePanelViewModel : ViewModelBase
    {
        public SidePanelViewModel(INavigationService navigationService)
        {
            NavigateWorkOrdersList = new NavigationCommand<WorkOrdersListViewModel>(navigationService).Navigate();
            NavigateOrdersList = new NavigationCommand<OrdersListViewModel>(navigationService).Navigate();
            NavigatePartsList = new NavigationCommand<PartsListViewModel>(navigationService).Navigate();
        }
        public ICommand NavigateWorkOrdersList { get; }
        public ICommand NavigateOrdersList { get; }
        public ICommand NavigatePartsList { get; }
    }
}