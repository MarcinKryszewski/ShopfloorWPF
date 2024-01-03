using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Plannist.ControlPanel;
using Shopfloor.Features.Dashboard;
using Shopfloor.Features.Plannist.Deploys;
using Shopfloor.Features.Plannist.Orders;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
using System;
using System.Windows.Input;

namespace Shopfloor.Layout.SidePanel
{
    public class SidePanelViewModel : ViewModelBase
    {
        public ICommand NavigateDashboardCommand { get; }
        public ICommand NavigateControlPanelCommand { get; }
        public ICommand NavigateOrdersCommand { get; }
        public ICommand NavigateDeploysCommand { get; }

        public SidePanelViewModel(IServiceProvider mainServices)
        {
            NavigateDashboardCommand = new NavigateCommand<DashboardViewModel>(mainServices.GetRequiredService<NavigationService<DashboardViewModel>>());
            NavigateControlPanelCommand = new NavigateCommand<ControlPanelViewModel>(mainServices.GetRequiredService<NavigationService<ControlPanelViewModel>>());
            NavigateOrdersCommand = new NavigateCommand<OrdersViewModel>(mainServices.GetRequiredService<NavigationService<OrdersViewModel>>());
            NavigateDeploysCommand = new NavigateCommand<DeploysViewModel>(mainServices.GetRequiredService<NavigationService<DeploysViewModel>>());
        }
    }
}