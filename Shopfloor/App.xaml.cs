using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Database;
using Shopfloor.Database.Configuration;
using Shopfloor.Database.Initializers;
using Shopfloor.Features.Manager.ManagerDashboard;
using Shopfloor.Features.Mechanic;
using Shopfloor.Features.Plannist;
using Shopfloor.Hosts;
using Shopfloor.Layout.Content;
using Shopfloor.Layout.MainWindow;
using Shopfloor.Layout.SidePanel;
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Stores;
using System;
using System.Data;
using System.Windows;

namespace Shopfloor
{
    public partial class App : Application
    {
        private readonly IHost _appHost;
        private readonly CurrentUserStore _currentUser;
        private readonly NavigationService _navigationService;
        private readonly IServiceProvider _services;

        public App()
        {
            _appHost = AppHost.Get();
            _appHost.Start();

            _services = _appHost.Services;

            _currentUser = _services.GetRequiredService<CurrentUserStore>();
            _navigationService = _services.GetRequiredService<NavigationService>();
        }
        private void ApplicationStart(object sender, StartupEventArgs e)
        {
            DatabaseConnectionFactory dbConnection = _services.GetRequiredService<DatabaseConnectionFactory>();
            DatabaseConfiguration dbConfig = _services.GetRequiredService<DatabaseConfiguration>();
            DatabaseInit(dbConnection, dbConfig);

            SidePanelViewModel sidePanel = _services.GetRequiredService<SidePanelViewModel>();
            ContentViewModel content = _services.GetRequiredService<ContentViewModel>();

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(sidePanel, content)
            };
            MainWindow.Show();

            AutoLogin(_currentUser);
            DashboardNavigate(_currentUser, _navigationService);
        }
        private static void AutoLogin(CurrentUserStore currentUserStore)
        {
            string userName = Environment.UserName;
            currentUserStore.Login(userName, true);
        }
        private static void DashboardNavigate(CurrentUserStore currentUserStore, NavigationService navigationService)
        {
            if (currentUserStore.User is null)
            {
                navigationService.NavigateTo<MechanicDashboardViewModel>();
                return;
            }
            if (currentUserStore.User.IsAuthorized(777))
            {
                navigationService.NavigateTo<ManagerDashboardViewModel>();
                return;
            }
            if (currentUserStore.User.IsAuthorized(460))
            {
                navigationService.NavigateTo<PlannistDashboardViewModel>();
                return;
            }
            navigationService.NavigateTo<MechanicDashboardViewModel>();
        }
        private static void DatabaseInit(DatabaseConnectionFactory dbConnectionFactory, DatabaseConfiguration dbConfiguration)
        {
            using IDbConnection connection = dbConnectionFactory.Connect();
            DatabaseInitializerFactory initializer = new(dbConfiguration, connection);
            IDatabaseInitializer databaseInitializer = initializer.CreateInitializer();
            databaseInitializer.Initialize();
        }
    }
}