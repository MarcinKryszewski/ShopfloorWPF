using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Machines;
using Shopfloor.Features.Admin.Parts;
using Shopfloor.Features.Admin.PartTypes;
using Shopfloor.Features.Admin.Suppliers;
using Shopfloor.Features.Admin.Users;
using Shopfloor.Features.Dashboard;
using Shopfloor.Features.Mechanic.Errands;
using Shopfloor.Features.Mechanic.MinimalStates;
using Shopfloor.Features.Mechanic.Requests;
using Shopfloor.Features.Plannist.PlannistDashboard;
using Shopfloor.Features.Plannist.Deploys;
using Shopfloor.Features.Plannist.Orders;
using Shopfloor.Features.Plannist.Reports;
using Shopfloor.Features.Plannist.Reservations;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.MachinePartModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Shopfloor.Features.Mechanic.PartsStock;
using Shopfloor.Features.Plannist.Offers;

namespace Shopfloor.Layout.SidePanel
{
    internal sealed partial class SidePanelViewModel : ViewModelBase
    {
        private readonly CurrentUserStore _userStore;
        private User? User => _userStore.User;

        #region Commands

        #region Dashboard

        public ICommand NavigateDashboardCommand { get; }

        #endregion Dashboard

        #region Mechanic

        public ICommand NavigateTasksCommand { get; }
        public ICommand NavigateRequestsCommand { get; }
        public ICommand NavigateMinimalStatesCommand { get; }
        public ICommand NavigatePartStockCommand { get; }

        #endregion Mechanic

        #region Plannist

        public ICommand NavigatePlannistDashboardMainCommand { get; }
        public ICommand NavigateOffersCommand { get; }
        public ICommand NavigateOrdersCommand { get; }
        public ICommand NavigateDeploysCommand { get; }
        public ICommand NavigateReservationsCommand { get; }
        public ICommand NavigateReportsCommand { get; }

        #endregion Plannist

        #region Admin

        public ICommand NavigateUsersCommand { get; }
        public ICommand NavigateMachinesCommand { get; }
        public ICommand NavigatePartsCommand { get; }
        public ICommand NavigateSuppliersCommand { get; }
        public ICommand NavigatePartTypesCommand { get; }

        #endregion Admin

        #endregion Commands

        public Visibility HasAdminRole => AdminRole();
        public Visibility HasPlannistRole => PlannistRole();
        public Visibility HasManagerRole => ManagerRole();
        public Visibility HasUserRole => UserRole();

        public SidePanelViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            NavigateDashboardCommand = new NavigateCommand<DashboardViewModel>(mainServices.GetRequiredService<NavigationService<DashboardViewModel>>());

            NavigateTasksCommand = new NavigateCommand<ErrandsMainViewModel>(mainServices.GetRequiredService<NavigationService<ErrandsMainViewModel>>());
            NavigateRequestsCommand = new NavigateCommand<RequestsMainViewModel>(mainServices.GetRequiredService<NavigationService<RequestsMainViewModel>>());
            NavigateMinimalStatesCommand = new NavigateCommand<MinimalStatesViewModel>(mainServices.GetRequiredService<NavigationService<MinimalStatesViewModel>>());
            NavigatePartStockCommand = new NavigateCommand<PartsStockMainViewModel>(mainServices.GetRequiredService<NavigationService<PartsStockMainViewModel>>());

            NavigatePlannistDashboardMainCommand = new NavigateCommand<PlannistDashboardMainViewModel>(mainServices.GetRequiredService<NavigationService<PlannistDashboardMainViewModel>>());
            NavigateOffersCommand = new NavigateCommand<OffersViewModel>(mainServices.GetRequiredService<NavigationService<OffersViewModel>>());
            NavigateOrdersCommand = new NavigateCommand<OrdersViewModel>(mainServices.GetRequiredService<NavigationService<OrdersViewModel>>());
            NavigateDeploysCommand = new NavigateCommand<DeploysViewModel>(mainServices.GetRequiredService<NavigationService<DeploysViewModel>>());
            NavigateReservationsCommand = new NavigateCommand<ReservationsViewModel>(mainServices.GetRequiredService<NavigationService<ReservationsViewModel>>());
            NavigateReportsCommand = new NavigateCommand<ReportsViewModel>(mainServices.GetRequiredService<NavigationService<ReportsViewModel>>());

            NavigateUsersCommand = new NavigateCommand<UsersMainViewModel>(mainServices.GetRequiredService<NavigationService<UsersMainViewModel>>());
            NavigateMachinesCommand = new NavigateCommand<MachinesMainViewModel>(mainServices.GetRequiredService<NavigationService<MachinesMainViewModel>>());
            NavigatePartsCommand = new NavigateCommand<PartsMainViewModel>(mainServices.GetRequiredService<NavigationService<PartsMainViewModel>>());
            NavigateSuppliersCommand = new NavigateCommand<SuppliersMainViewModel>(mainServices.GetRequiredService<NavigationService<SuppliersMainViewModel>>());
            NavigatePartTypesCommand = new NavigateCommand<PartTypesMainViewModel>(mainServices.GetRequiredService<NavigationService<PartTypesMainViewModel>>());

            _userStore = mainServices.GetRequiredService<CurrentUserStore>();
            _userStore.PropertyChanged += OnUserAuthenticated;

            _dbServices = databaseServices;
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
            if (User is null) return Visibility.Collapsed;
            if (User.IsAuthorized(777)) return Visibility.Visible;
            return Visibility.Collapsed;
        }

        private Visibility PlannistRole()
        {
            if (User is null) return Visibility.Collapsed;
            if (User.IsAuthorized(460)) return Visibility.Visible;
            return Visibility.Collapsed;
        }

        private Visibility ManagerRole()
        {
            if (User is null) return Visibility.Collapsed;
            if (User.IsAuthorized(205)) return Visibility.Visible;
            return Visibility.Collapsed;
        }

        private Visibility UserRole()
        {
            if (User is null) return Visibility.Collapsed;
            if (User.IsAuthorized(568)) return Visibility.Visible;
            return Visibility.Collapsed;
        }
    }
    internal sealed partial class SidePanelViewModel
    {
        public ICommand AddTestData => new TestDataCommand(this, _dbServices);
        private IServiceProvider _dbServices;
        public Visibility HasDataInside { get; private set; } = Visibility.Visible;
        private class TestDataCommand : CommandBase
        {
            private readonly SidePanelViewModel _sidePanelViewModel;
            private readonly IServiceProvider _dbServices;
            public TestDataCommand(SidePanelViewModel sidePanelViewModel, IServiceProvider dbServices)
            {
                _sidePanelViewModel = sidePanelViewModel;
                _dbServices = dbServices;
            }
            public override void Execute(object? parameter)
            {
                System.Diagnostics.Debug.Write("TEST");

                PartTypeProvider partTypesProvider = _dbServices.GetRequiredService<PartTypeProvider>();
                _ = partTypesProvider.Create(new PartType("Łożysko"));
                _ = partTypesProvider.Create(new PartType("Koło"));

                MachineProvider machineProvider = _dbServices.GetRequiredService<MachineProvider>();
                _ = machineProvider.Create(new Machine("Monoblok", "sdfsd", null, null, true));
                _ = machineProvider.Create(new Machine("Etykieciarka", "sdfsd", null, null, true));

                SupplierProvider supplierProvider = _dbServices.GetRequiredService<SupplierProvider>();
                _ = supplierProvider.Create(new Supplier("Krones", true));

                PartProvider partProvider = _dbServices.GetRequiredService<PartProvider>();
                _ = partProvider.Create(new Part("3002", "bearing 3002", 1, 50004413, "9453210", null, 1, 1));
                _ = partProvider.Create(new Part("21Z D30", "sprocket", 2, 215463, "ZA12354fd", null, 1, 1));

                MachinePartProvider machinePartProvider = _dbServices.GetRequiredService<MachinePartProvider>();
                _ = machinePartProvider.Create(new MachinePart(1, 1, 5));
                _ = machinePartProvider.Create(new MachinePart(1, 2, 7));
                _ = machinePartProvider.Create(new MachinePart(2, 1, 3));

                ErrandProvider errandProvider = _dbServices.GetRequiredService<ErrandProvider>();
                _ = errandProvider.Create(new Errand(DateTime.Now, 2, 1, 1, "Awaria", "A"));
                _ = errandProvider.Create(new Errand(DateTime.Now, 3, 2, 2, "Cilty robienie", "C"));
                _ = errandProvider.Create(new Errand(DateTime.Now, 2, 1, 4, "Naprawa czegośtam", "B"));
                _ = errandProvider.Create(new Errand(DateTime.Now, 2, 2, 3, "Modyfikacja cośtam"));

                ErrandStatusProvider errandStatusProvider = _dbServices.GetRequiredService<ErrandStatusProvider>();
                _ = errandStatusProvider.Create(new ErrandStatus(1, "Completed", "No issues found.", "All tasks done.", DateTime.Now));
                _ = errandStatusProvider.Create(new ErrandStatus(1, "Pending", null, null, new DateTime(2024, 1, 15)));
                _ = errandStatusProvider.Create(new ErrandStatus(2, "InProgress", "Work in progress", "Awaiting approval", null));
                _ = errandStatusProvider.Create(new ErrandStatus(2, "Cancelled", null, null, new DateTime(2024, 2, 16)));
                _ = errandStatusProvider.Create(new ErrandStatus(3, "Delayed", "Task postponed due to inclement weather", "Weather conditions", null));
                _ = errandStatusProvider.Create(new ErrandStatus(4, "Scheduled", "Task planned for next week", "Upcoming schedule", new DateTime(2024, 2, 17)));

                ErrandPartProvider errandPartProvider = _dbServices.GetRequiredService<ErrandPartProvider>();
                _ = errandPartProvider.Create(new ErrandPart(1, 1, 10.5, 1));
                _ = errandPartProvider.Create(new ErrandPart(1, 2, null, 2));
                _ = errandPartProvider.Create(new ErrandPart(4, 1, 7.25, 1));
                _ = errandPartProvider.Create(new ErrandPart(4, 2, 3.0, 1));
                _ = errandPartProvider.Create(new ErrandPart(1, 1, 1.0, 2));
                _ = errandPartProvider.Create(new ErrandPart(2, 1, 5.0, 3));
                _ = errandPartProvider.Create(new ErrandPart(3, 2, 8.0, 2));

                ErrandPartStatusProvider errandPartStatusProvider = _dbServices.GetRequiredService<ErrandPartStatusProvider>();
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(7, 1, 0, DateTime.Now.AddDays(-17)));
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(1, 2, 2, DateTime.Now.AddDays(-3), "In progress"));
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(4, 3, 5, DateTime.Now.AddDays(-8), "Pending", "Waiting for approval"));
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(5, 1, 3, DateTime.Now.AddDays(-12), reason: null));
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(6, 2, 1, DateTime.Now.AddDays(-21), "Completed", "User feedback received"));
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(2, 3, 4, DateTime.Now.AddDays(-5), "Delayed", "Supplier issue"));
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(3, 1, 5, DateTime.Now.AddDays(-10), "Cancelled"));
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(1, 2, 0, DateTime.Now.AddDays(-14), reason: "SYSTEM"));
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(2, 3, 1, DateTime.Now.AddDays(-19), reason: "SYSTEM"));
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(3, 1, 3, DateTime.Now.AddDays(-22), "Completed"));
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(4, 2, 4, DateTime.Now.AddDays(-7)));
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(5, 3, 5, DateTime.Now.AddDays(-9), "In progress", "Technical issue"));
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(6, 1, 0, DateTime.Now.AddDays(-15)));
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(7, 2, 1, DateTime.Now.AddDays(-20), "In progress"));
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(1, 3, 2, DateTime.Now.AddDays(-25), "Pending", "Waiting for approval"));

                _sidePanelViewModel.HasDataInside = Visibility.Collapsed;
                _sidePanelViewModel.OnPropertyChanged(nameof(HasDataInside));
            }
        }
    }
}