using System;
using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrzegladyRemonty.Database;

namespace Shopfloor
{
    public partial class App : Application
    {
        private readonly IHost _configurationHost;
        private readonly IHost _databaseHost;
        private readonly IConfiguration _configuration;

        public App()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _configurationHost = Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton(_configuration);
            })
            .Build();

            _databaseHost = Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton(new DatabaseConnectionFactory(_configurationHost.Services));
                ProvidersServices(services);
            })
            .Build();


        }

        private void ApplicationStart(object sender, StartupEventArgs e)
        {
            _configurationHost.Start();
            _databaseHost.Start();
        }

        private static void ProvidersServices(IServiceCollection services)
        {
        }
    }
}
