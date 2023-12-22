using System;
using System.Data;
using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Database;
using Shopfloor.Database.Initializers;
using Shopfloor.Hosts.ConfigurationHost;
using Shopfloor.Hosts.DatabaseHost;

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
            _databaseHost = DatabaseHost.GetHost(_configurationHost.Services);
        }

        private void ApplicationStart(object sender, StartupEventArgs e)
        {
            _configurationHost.Start();
            _databaseHost.Start();

            using IDbConnection connection = _databaseHost.Services.GetRequiredService<DatabaseConnectionFactory>().Connect();
            DatabaseInitializerFactory initializer = new(_configurationHost.Services, connection);
            IDatabaseInitializer databaseInitializer = initializer.CreateInitializer();
            databaseInitializer.Initialize();
        }
    }
}
