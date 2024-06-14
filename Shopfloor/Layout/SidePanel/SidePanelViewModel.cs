using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Machines;
using Shopfloor.Features.Admin.Parts;
using Shopfloor.Features.Admin.PartTypes;
using Shopfloor.Features.Admin.Suppliers;
using Shopfloor.Features.Admin.Users;
using Shopfloor.Features.Manager.ManagerDashboard;
using Shopfloor.Features.Manager.OrdersToApprove;
using Shopfloor.Features.Mechanic;
using Shopfloor.Features.Mechanic.Errands;
using Shopfloor.Features.Mechanic.PartsStock;
using Shopfloor.Features.Mechanic.Requests;
using Shopfloor.Features.Plannist;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.MachinePartModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;

namespace Shopfloor.Layout.SidePanel
{
    internal sealed partial class SidePanelViewModel : ViewModelBase
    {
        private readonly ICurrentUserStore _userStore;
        private User? _user => _userStore.User;

        #region Mechanic
        public ICommand NavigateMechanicDashboardCommand { get; }
        public ICommand NavigateTasksCommand { get; }
        public ICommand NavigateRequestsCommand { get; }
        public ICommand NavigatePartStockCommand { get; }
        #endregion Mechanic

        #region Plannist
        public ICommand NavigatePlannistDashboardCommand { get; }
        public ICommand NavigateOffersCommand { get; }
        public ICommand NavigateOrdersCommand { get; }
        public ICommand NavigateDeploysCommand { get; }
        #endregion Plannist

        #region Manager
        public ICommand NavigateManagerDashboardCommand { get; }
        public ICommand NavigateOrdersToApproveCommand { get; }
        #endregion Manager

        #region Admin
        public ICommand NavigateUsersCommand { get; }
        public ICommand NavigateMachinesCommand { get; }
        public ICommand NavigatePartsCommand { get; }
        public ICommand NavigateSuppliersCommand { get; }
        public ICommand NavigatePartTypesCommand { get; }
        #endregion Admin

        public Visibility HasAdminRole => HasAuthorization(Roles.Admin);
        public Visibility HasPlannistRole => HasAuthorization(Roles.Plannist);
        public Visibility HasManagerRole => HasAuthorization(Roles.Manager);
        public Visibility HasUserRole => HasAuthorization(Roles.User);
        public SidePanelViewModel(INavigationService navigationService, ICurrentUserStore userStore, IServiceProvider databaseServices)
        {
            NavigateMechanicDashboardCommand = new NavigationCommand<MechanicDashboardViewModel>(navigationService).Navigate();

            NavigateTasksCommand = new NavigationCommand<ErrandsListViewModel>(navigationService).Navigate();
            NavigateRequestsCommand = new NavigationCommand<RequestsListViewModel>(navigationService).Navigate();
            NavigatePartStockCommand = new NavigationCommand<PartsStockListViewModel>(navigationService).Navigate();

            NavigateDeploysCommand = new NavigationCommand<DeploysViewModel>(navigationService).Navigate();
            NavigateOffersCommand = new NavigationCommand<OffersViewModel>(navigationService).Navigate();
            NavigateOrdersCommand = new NavigationCommand<PartsOrdersViewModel>(navigationService).Navigate();
            NavigatePlannistDashboardCommand = new NavigationCommand<PlannistPartsListViewModel>(navigationService).Navigate();

            NavigateManagerDashboardCommand = new NavigationCommand<ManagerDashboardViewModel>(navigationService).Navigate();
            NavigateOrdersToApproveCommand = new NavigationCommand<OrdersToApproveViewModel>(navigationService).Navigate();

            NavigateUsersCommand = new NavigationCommand<UsersListViewModel>(navigationService).Navigate();
            NavigateMachinesCommand = new NavigationCommand<MachinesListViewModel>(navigationService).Navigate();
            NavigatePartsCommand = new NavigationCommand<PartsListViewModel>(navigationService).Navigate();
            NavigateSuppliersCommand = new NavigationCommand<SuppliersListViewModel>(navigationService).Navigate();
            NavigatePartTypesCommand = new NavigationCommand<PartTypesListViewModel>(navigationService).Navigate();

            _userStore = userStore;
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
        private Visibility HasAuthorization(int authVal)
        {
            if (_user is null) return Visibility.Collapsed;
            if (_user.IsAuthorized(authVal)) return Visibility.Visible;
            return Visibility.Collapsed;
        }
    }
    [ExcludeFromCodeCoverage]
    internal sealed partial class SidePanelViewModel
    {
        public ICommand AddTestData => new TestDataCommand(this, _dbServices);
        private readonly IServiceProvider _dbServices;
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
                //string name, string number, string? sapNumber, int? parent, bool isActive
                _ = machineProvider.Create(new Machine()
                {
                    Name = "Monoblok",
                    Number = "sdfsd",
                    IsActive = true,
                });
                _ = machineProvider.Create(new Machine()
                {
                    Name = "Etykieciarka",
                    Number = "sdfsd",
                    IsActive = true,
                });

                SupplierProvider supplierProvider = _dbServices.GetRequiredService<SupplierProvider>();
                _ = supplierProvider.Create(new Supplier("Krones", true));

                PartProvider partProvider = _dbServices.GetRequiredService<PartProvider>();
                _ = partProvider.Create(new Part()
                {
                    NamePl = "3002",
                    NameOriginal = "bearing 3002",
                    TypeId = 1,
                    Index = 50004413,
                    ProducerNumber = "9453210",
                    ProducerId = 1,
                    SupplierId = 1
                });
                _ = partProvider.Create(new Part()
                {
                    NamePl = "21Z D30",
                    NameOriginal = "sprocket",
                    TypeId = 2,
                    Index = 215463,
                    ProducerNumber = "ZA12354fd",
                    ProducerId = 1,
                    SupplierId = 1
                });

                MachinePartProvider machinePartProvider = _dbServices.GetRequiredService<MachinePartProvider>();
                _ = machinePartProvider.Create(new MachinePart()
                {
                    Amount = 5,
                    MachineId = 1,
                    PartId = 1,
                });
                _ = machinePartProvider.Create(new MachinePart()
                {
                    Amount = 7,
                    PartId = 1,
                    MachineId = 2,
                });
                _ = machinePartProvider.Create(new MachinePart()
                {
                    Amount = 3,
                    PartId = 2,
                    MachineId = 1,
                });

                ErrandProvider errandProvider = _dbServices.GetRequiredService<ErrandProvider>();
                _ = errandProvider.Create(new Errand()
                {
                    CreatedDate = DateTime.Now,
                    CreatedById = 2,
                    Description = "Awaria",
                    Priority = "A",
                    MachineId = 1,
                    TypeId = 1,
                });
                _ = errandProvider.Create(new Errand()
                {
                    CreatedDate = DateTime.Now,
                    CreatedById = 3,
                    Description = "Cilty robienie",
                    Priority = "C",
                    MachineId = 2,
                    TypeId = 2,
                });
                _ = errandProvider.Create(new Errand()
                {
                    CreatedDate = DateTime.Now,
                    CreatedById = 2,
                    Description = "Naprawa czegośtam",
                    Priority = "B",
                    MachineId = 1,
                    TypeId = 4,
                });
                _ = errandProvider.Create(new Errand()
                {
                    CreatedDate = DateTime.Now,
                    CreatedById = 2,
                    Description = "Modyfikacja cośtam",
                    MachineId = 2,
                    TypeId = 3,
                });

                ErrandStatusProvider errandStatusProvider = _dbServices.GetRequiredService<ErrandStatusProvider>();
                //int errandId, string statusName, string? comment, string? reason, DateTime? setDate
                _ = errandStatusProvider.Create(new ErrandStatus()
                {
                    ErrandId = 1,
                    StatusName = "Completed",
                    Comment = "No issues found.",
                    Reason = "All tasks done.",
                    SetDate = DateTime.Now,
                });
                _ = errandStatusProvider.Create(new ErrandStatus()
                {
                    ErrandId = 1,
                    StatusName = "Pending",
                    SetDate = new DateTime(2024, 1, 15),
                });
                _ = errandStatusProvider.Create(new ErrandStatus()
                {
                    ErrandId = 2,
                    StatusName = "InProgress",
                    Comment = "Work in progress",
                    Reason = "Awaiting approval",
                    SetDate = DateTime.Now,
                });
                _ = errandStatusProvider.Create(new ErrandStatus()
                {
                    ErrandId = 2,
                    StatusName = "Cancelled",
                    SetDate = new DateTime(2024, 2, 16),
                });
                _ = errandStatusProvider.Create(new ErrandStatus()
                {
                    ErrandId = 3,
                    StatusName = "Delayed",
                    Comment = "Task postponed due to inclement weather",
                    Reason = "Weather conditions",
                    SetDate = DateTime.Now
                });
                _ = errandStatusProvider.Create(new ErrandStatus()
                {
                    ErrandId = 4,
                    StatusName = "Scheduled",
                    Comment = "Task planned for next week",
                    Reason = "Upcoming schedule",
                    SetDate = new DateTime(2024, 2, 17),
                });

                ErrandPartProvider errandPartProvider = _dbServices.GetRequiredService<ErrandPartProvider>();
                _ = errandPartProvider.Create(new ErrandPart() { ErrandId = 1, PartId = 1, Amount = 10.5, OrderedById = 1 });
                _ = errandPartProvider.Create(new ErrandPart() { ErrandId = 1, PartId = 2, Amount = null, OrderedById = 2 });
                _ = errandPartProvider.Create(new ErrandPart() { ErrandId = 4, PartId = 1, Amount = 7.25, OrderedById = 1 });
                _ = errandPartProvider.Create(new ErrandPart() { ErrandId = 4, PartId = 2, Amount = 3.0, OrderedById = 1 });
                _ = errandPartProvider.Create(new ErrandPart() { ErrandId = 1, PartId = 1, Amount = 1.0, OrderedById = 2 });
                _ = errandPartProvider.Create(new ErrandPart() { ErrandId = 2, PartId = 1, Amount = 5.0, OrderedById = 3 });
                _ = errandPartProvider.Create(new ErrandPart() { ErrandId = 3, PartId = 2, Amount = 8.0, OrderedById = 2 });

                ErrandPartStatusProvider errandPartStatusProvider = _dbServices.GetRequiredService<ErrandPartStatusProvider>();
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(0)
                {
                    ErrandPartId = 7,
                    CompletedById = 1,
                    CreatedDate = DateTime.Now.AddDays(-17),
                });
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(2)
                {
                    ErrandPartId = 1,
                    CompletedById = 2,
                    CreatedDate = DateTime.Now.AddDays(-3),
                    Comment = "In progress",
                });
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(5)
                {
                    ErrandPartId = 4,
                    CompletedById = 3,
                    CreatedDate = DateTime.Now.AddDays(-8),
                    Comment = "Pending",
                    Reason = "Waiting for approval",
                });
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(3)
                {
                    ErrandPartId = 5,
                    CompletedById = 1,
                    CreatedDate = DateTime.Now.AddDays(-12)
                });
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(1)
                {
                    ErrandPartId = 6,
                    CompletedById = 2,
                    CreatedDate = DateTime.Now.AddDays(-21),
                    Comment = "Completed",
                    Reason = "User feedback received",
                });
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(4)
                {
                    ErrandPartId = 2,
                    CompletedById = 3,
                    CreatedDate = DateTime.Now.AddDays(-5),
                    Comment = "Delayed",
                    Reason = "Supplier issue",
                });
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(5)
                {
                    ErrandPartId = 3,
                    CompletedById = 1,
                    CreatedDate = DateTime.Now.AddDays(-10),
                    Comment = "Cancelled",
                });
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(0)
                {
                    ErrandPartId = 1,
                    CompletedById = 2,
                    CreatedDate = DateTime.Now.AddDays(-14),
                    Reason = "SYSTEM",
                });
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(1)
                {
                    ErrandPartId = 2,
                    CompletedById = 3,
                    CreatedDate = DateTime.Now.AddDays(-19),
                    Reason = "SYSTEM",
                });
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(3)
                {
                    ErrandPartId = 3,
                    CompletedById = 1,
                    CreatedDate = DateTime.Now.AddDays(-22),
                    Comment = "Completed",
                });
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(4)
                {
                    ErrandPartId = 4,
                    CompletedById = 2,
                    CreatedDate = DateTime.Now.AddDays(-7),
                });
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(5)
                {
                    ErrandPartId = 5,
                    CompletedById = 3,
                    CreatedDate = DateTime.Now.AddDays(-9),
                    Comment = "In progress",
                    Reason = "Technical issue",
                }); ;
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(0)
                {
                    ErrandPartId = 6,
                    CompletedById = 1,
                    CreatedDate = DateTime.Now.AddDays(-15),
                });
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(1)
                {
                    ErrandPartId = 7,
                    CompletedById = 2,
                    CreatedDate = DateTime.Now.AddDays(-20),
                    Comment = "In progress",
                });
                _ = errandPartStatusProvider.Create(new ErrandPartStatus(2)
                {
                    ErrandPartId = 1,
                    CompletedById = 3,
                    CreatedDate = DateTime.Now.AddDays(-25),
                    Comment = "Pending",
                    Reason = "Waiting for approval",
                });

                _sidePanelViewModel.HasDataInside = Visibility.Collapsed;
                _sidePanelViewModel.OnPropertyChanged(nameof(HasDataInside));
            }
        }
    }
}