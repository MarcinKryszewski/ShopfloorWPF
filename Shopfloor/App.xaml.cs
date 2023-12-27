using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Database;
using Shopfloor.Database.Initializers;
using Shopfloor.Hosts.ConfigurationHost;
using Shopfloor.Hosts.DatabaseHost;
using Shopfloor.Layout.MainWindow;
using System.Data;
using System.Windows;

namespace Shopfloor
{
    public partial class App : Application
    {
        private readonly IHost _configurationHost;
        private readonly IHost _databaseHost;

        public App()
        {
            ConfigurationHost configuration = new();
            _configurationHost = configuration.GetHost();
            _configurationHost.Start();
            _databaseHost = DatabaseHost.GetHost(_configurationHost.Services);
            _databaseHost.Start();
        }

        private void ApplicationStart(object sender, StartupEventArgs e)
        {
            using IDbConnection connection = _databaseHost.Services.GetRequiredService<DatabaseConnectionFactory>().Connect();
            DatabaseInitializerFactory initializer = new(_configurationHost.Services, connection);
            IDatabaseInitializer databaseInitializer = initializer.CreateInitializer();
            databaseInitializer.Initialize();

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel()
            };

            MainWindow.Show();
        }
    }
}
