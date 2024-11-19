using System;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Database;
using Shopfloor.Database.Configuration;
using Shopfloor.Database.Initializers;
using Shopfloor.Features.God;
using Shopfloor.Hosts;
using Shopfloor.Layout.Content;
using Shopfloor.Layout.MainWindow;
using Shopfloor.Layout.SidePanel;
using Shopfloor.Services.NavigationServices;

namespace Shopfloor
{
    public partial class App : Application
    {
        private readonly IHost _appHost;
        private readonly NavigationService _navigationService;
        private readonly IServiceProvider _services;

        public App()
        {
            _appHost = AppHost.Get();
            _appHost.Start();

            _services = _appHost.Services;

            _navigationService = _services.GetRequiredService<NavigationService>();
        }
        private static void DatabaseInit(DatabaseConnectionFactory dbConnectionFactory, DatabaseConfiguration dbConfiguration)
        {
            using IDbConnection connection = dbConnectionFactory.Connect();
            DatabaseInitializerFactory initializer = new(dbConfiguration, connection);
            IDatabaseInitializer databaseInitializer = initializer.CreateInitializer();
            databaseInitializer.Initialize();
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
                DataContext = new MainWindowViewModel(sidePanel, content),
            };
            MainWindow.Show();

            _navigationService.NavigateTo<GodViewModel>();
        }
    }
}