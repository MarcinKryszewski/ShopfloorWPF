using System;
using System.Windows.Input;
using Shopfloor.Features.PartsList;
using Shopfloor.Features.WorkOrdersList;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Layout.SidePanel
{
    internal sealed partial class SidePanelViewModel : ViewModelBase
    {
        public SidePanelViewModel(INavigationService navigationService, IServiceProvider databaseServices)
        {
            NavigateWorkOrdersList = new NavigationCommand<WorkOrdersListViewModel>(navigationService).Navigate();
            NavigatePartsList = new NavigationCommand<PartsListViewModel>(navigationService).Navigate();

        }
        public ICommand NavigateWorkOrdersList { get; }
        public ICommand NavigatePartsList { get; }
    }
}