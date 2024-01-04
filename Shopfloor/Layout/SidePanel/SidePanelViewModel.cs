using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Machines;
using Shopfloor.Features.Admin.Parts;
using Shopfloor.Features.Admin.Users;
using Shopfloor.Features.Dashboard;
using Shopfloor.Features.Mechanic.MinimalStates;
using Shopfloor.Features.Mechanic.Requests;
using Shopfloor.Features.Mechanic.Tasks;
using Shopfloor.Features.Plannist.ControlPanel;
using Shopfloor.Features.Plannist.Deploys;
using Shopfloor.Features.Plannist.Orders;
using Shopfloor.Features.Plannist.Reports;
using Shopfloor.Features.Plannist.Reservations;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
using System;
using System.Windows.Input;

namespace Shopfloor.Layout.SidePanel
{
    public class SidePanelViewModel : ViewModelBase
    {
        #region Commands
        #region Dashboard
        public ICommand NavigateDashboardCommand { get; }
        #endregion
        #region Mechanic
        public ICommand NavigateTasksCommand { get; }
        public ICommand NavigateRequestsCommand { get; }
        public ICommand NavigateMinimalStatesCommand { get; }
        #endregion
        #region Plannist
        public ICommand NavigateControlPanelCommand { get; }
        public ICommand NavigateOrdersCommand { get; }
        public ICommand NavigateDeploysCommand { get; }
        public ICommand NavigateReservationsCommand { get; }
        public ICommand NavigateReportsCommand { get; }
        #endregion
        #region Admin
        public ICommand NavigateUsersCommand { get; }
        public ICommand NavigateMachinesCommand { get; }
        public ICommand NavigatePartsCommand { get; }
        #endregion
        #endregion

        public SidePanelViewModel(IServiceProvider mainServices)
        {
            NavigateDashboardCommand = new NavigateCommand<DashboardViewModel>(mainServices.GetRequiredService<NavigationService<DashboardViewModel>>());

            NavigateTasksCommand = new NavigateCommand<TasksViewModel>(mainServices.GetRequiredService<NavigationService<TasksViewModel>>());
            NavigateRequestsCommand = new NavigateCommand<RequestsViewModel>(mainServices.GetRequiredService<NavigationService<RequestsViewModel>>());
            NavigateMinimalStatesCommand = new NavigateCommand<MinimalStatesViewModel>(mainServices.GetRequiredService<NavigationService<MinimalStatesViewModel>>());

            NavigateControlPanelCommand = new NavigateCommand<ControlPanelViewModel>(mainServices.GetRequiredService<NavigationService<ControlPanelViewModel>>());
            NavigateOrdersCommand = new NavigateCommand<OrdersViewModel>(mainServices.GetRequiredService<NavigationService<OrdersViewModel>>());
            NavigateDeploysCommand = new NavigateCommand<DeploysViewModel>(mainServices.GetRequiredService<NavigationService<DeploysViewModel>>());
            NavigateReservationsCommand = new NavigateCommand<ReservationsViewModel>(mainServices.GetRequiredService<NavigationService<ReservationsViewModel>>());
            NavigateReportsCommand = new NavigateCommand<ReportsViewModel>(mainServices.GetRequiredService<NavigationService<ReportsViewModel>>());

            NavigateUsersCommand = new NavigateCommand<UsersViewModel>(mainServices.GetRequiredService<NavigationService<UsersViewModel>>());
            NavigateMachinesCommand = new NavigateCommand<MachinesViewModel>(mainServices.GetRequiredService<NavigationService<MachinesViewModel>>());
            NavigatePartsCommand = new NavigateCommand<PartsViewModel>(mainServices.GetRequiredService<NavigationService<PartsViewModel>>());
        }
    }
}