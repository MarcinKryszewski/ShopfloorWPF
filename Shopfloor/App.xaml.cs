using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Database;
using Shopfloor.Database.Initializers;
using Shopfloor.Features.Manager.ManagerDashboard;
using Shopfloor.Features.Mechanic.MechanicDashboard;
using Shopfloor.Features.Plannist.PlannistDashboard;
using Shopfloor.Hosts;
using Shopfloor.Hosts.ConfigurationHost;
using Shopfloor.Hosts.DatabaseHost;
using Shopfloor.Hosts.MainHost;
using Shopfloor.Layout.MainWindow;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Services;
using Shopfloor.Stores;
using System;
using System.Data;
using System.Windows;
using ToastNotifications;

namespace Shopfloor
{
    public partial class App : Application
    {
        private readonly IHost _configurationHost;
        private readonly IHost _databaseHost;
        private readonly IHost _mainHost;
        private readonly IHost _userHost;

        public App()
        {
            ConfigurationHost configuration = new();
            _configurationHost = configuration.GetHost();
            _configurationHost.Start();

            _databaseHost = DatabaseHost.GetHost(_configurationHost.Services);
            _databaseHost.Start();

            _userHost = UserHost.GetHost(_databaseHost.Services);
            _userHost.Start();

            _mainHost = MainHost.GetHost(_databaseHost.Services, _userHost.Services);
            _mainHost.Start();


        }

        private void ApplicationStart(object sender, StartupEventArgs e)
        {
            using IDbConnection connection = _databaseHost.Services.GetRequiredService<DatabaseConnectionFactory>().Connect();
            DatabaseInitializerFactory initializer = new(_configurationHost.Services, connection);
            IDatabaseInitializer databaseInitializer = initializer.CreateInitializer();
            databaseInitializer.Initialize();

            //tries to login user automatically
            _userHost.Services.GetRequiredService<CurrentUserStore>().AutoLogin(
                Environment.UserName,
                _databaseHost.Services.GetRequiredService<UserProvider>(),
                _mainHost.Services.GetRequiredService<Notifier>()
            );

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(_mainHost.Services)
            };
            MainWindow.Show();
            DashboardNavigate();

            //NavigationService<MechanicDashboardViewModel> navigationService = _mainHost.Services.GetRequiredService<NavigationService<MechanicDashboardViewModel>>();
            //navigationService.Navigate();
        }
        private void DashboardNavigate()
        {
            CurrentUserStore currentUser = _userHost.Services.GetRequiredService<CurrentUserStore>();
            if (currentUser.HasRole(777))
            {
                _mainHost.Services.GetRequiredService<NavigationService<ManagerDashboardViewModel>>().Navigate();
                return;
            }
            if (currentUser.HasRole(460))
            {
                _mainHost.Services.GetRequiredService<NavigationService<PlannistDashboardViewModel>>().Navigate();
                return;
            }
            _mainHost.Services.GetRequiredService<NavigationService<MechanicDashboardViewModel>>().Navigate();
        }
    }
}