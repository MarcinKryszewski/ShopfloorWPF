using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Database.Configuration;
using System;
using System.IO;

namespace Shopfloor.Hosts.Core
{
    internal sealed class ConfigurationHost
    {
        private readonly string _databaseType;
        private readonly string _databasePath;
        private readonly IConfiguration _configuration;

        public ConfigurationHost()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _databaseType = _configuration["DatabaseType"] ?? string.Empty;
            _databasePath = _configuration["DatabasePath"] ?? string.Empty;
        }

        public void Get(IServiceCollection services)
        {
            DatabaseConfiguration databaseConfiguration = new()
            {
                Type = _databaseType,
                Path = _databasePath,
                ConnectionString = GetConnectionString()
            };

            services.AddSingleton(_configuration);
            services.AddSingleton(databaseConfiguration);
        }

        private string GetConnectionString()
        {
            return _databaseType switch
            {
                "SQLite" => _configuration.GetConnectionString("SQLiteConnection") ?? string.Empty,
                _ => throw new InvalidOperationException("Invalid or unsupported database type."),
            };
        }
    }
}