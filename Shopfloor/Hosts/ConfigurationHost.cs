using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Database;
using Shopfloor.Database.Configuration;

namespace Shopfloor.Hosts.ConfigurationHost
{
    public class ConfigurationHost
    {
        private readonly string _databaseType;
        private readonly IConfiguration _configuration;

        public ConfigurationHost()
        {
            IConfiguration _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _databaseType = _configuration["DatabaseType"] ?? "";
        }
        public IHost GetHost()
        {
            DatabaseConfiguration databaseConfiguration = new()
            {
                Type = _databaseType,
                ConnectionString = GetConnectionString()
            };

            return Host
            .CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton(_configuration);
                services.AddSingleton(databaseConfiguration);
            })
            .Build();
        }

        private string GetConnectionString()
        {
            return _databaseType switch
            {
                "SQLite" => _configuration.GetConnectionString("SQLiteConnection") ?? "",
                _ => throw new InvalidOperationException("Invalid or unsupported database type."),
            };
        }
    }


}