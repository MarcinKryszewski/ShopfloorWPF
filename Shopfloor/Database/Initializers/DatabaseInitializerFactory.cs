using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Database.Configuration;
using System;
using System.Data;

namespace Shopfloor.Database.Initializers
{
    public class DatabaseInitializerFactory
    {
        private readonly IDbConnection _connection;
        private readonly DatabaseConfiguration _configuration;

        public DatabaseInitializerFactory(IServiceProvider service, IDbConnection connection)
        {
            _connection = connection;
            _configuration = service.GetRequiredService<DatabaseConfiguration>();
        }

        public IDatabaseInitializer CreateInitializer()
        {
            return _configuration.Type switch
            {
                "SQLite" => new SQLiteDatabaseInitializer(_connection, _configuration.Path ?? string.Empty),
                _ => throw new InvalidOperationException("Invalid or unsupported database type."),
            };
        }
    }
}
