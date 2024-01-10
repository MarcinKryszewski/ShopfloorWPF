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
using Shopfloor.Stores;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Shopfloor.Layout.SidePanel
{
    public class SidePanelViewModel : ViewModelBase
    {
        private readonly UserStore _userStore;

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

        public Visibility HasAdminRole => AdminRole();
        public Visibility HasPlannistRole => PlannistRole();
        public Visibility HasManagerRole => ManagerRole();
        public Visibility HasUserRole => UserRole();

        public SidePanelViewModel(IServiceProvider mainServices, IServiceProvider userServices)
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

            _userStore = userServices.GetRequiredService<UserStore>();
            _userStore.PropertyChanged += OnUserAuthenticated;
        }

        private void OnUserAuthenticated(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_userStore.IsUserLoggedIn))
            {
                OnPropertyChanged(nameof(HasAdminRole));
                OnPropertyChanged(nameof(HasManagerRole));
                OnPropertyChanged(nameof(HasPlannistRole));
                OnPropertyChanged(nameof(HasUserRole));
            }
        }

        private Visibility AdminRole()
        {
            if (_userStore.User.IsAuthorized(777)) return Visibility.Visible;
            return Visibility.Collapsed;
        }
        private Visibility PlannistRole()
        {
            if (_userStore.User.IsAuthorized(460)) return Visibility.Visible;
            return Visibility.Collapsed;
        }
        private Visibility ManagerRole()
        {
            if (_userStore.User.IsAuthorized(205)) return Visibility.Visible;
            return Visibility.Collapsed;
        }
        private Visibility UserRole()
        {
            if (_userStore.User.IsAuthorized(568)) return Visibility.Visible;
            return Visibility.Collapsed;
        }
    }
}