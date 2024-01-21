using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Database;
using Shopfloor.Database.Initializers;
using Shopfloor.Features.Dashboard;
using Shopfloor.Hosts;
using Shopfloor.Hosts.ConfigurationHost;
using Shopfloor.Hosts.DatabaseHost;
using Shopfloor.Hosts.MainHost;
using Shopfloor.Layout.MainWindow;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Services;
using Shopfloor.Stores;
using System;
using System.Data;
using System.Windows;

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

            NavigationService<DashboardViewModel> navigationService = _mainHost.Services.GetRequiredService<NavigationService<DashboardViewModel>>();
            navigationService.Navigate();
        }

        private void ApplicationStart(object sender, StartupEventArgs e)
        {
            using IDbConnection connection = _databaseHost.Services.GetRequiredService<DatabaseConnectionFactory>().Connect();
            DatabaseInitializerFactory initializer = new(_configurationHost.Services, connection);
            IDatabaseInitializer databaseInitializer = initializer.CreateInitializer();
            databaseInitializer.Initialize();

            //tries to login user automatically
            _userHost.Services.GetRequiredService<UserStore>().AutoLogin(
                Environment.UserName,
                _databaseHost.Services.GetRequiredService<UserProvider>()
            );

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(_mainHost.Services)
            };

            MainWindow.Show();
        }
    }
}