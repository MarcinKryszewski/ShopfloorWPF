using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Database.Configuration;
using System;
using System.Data;

namespace Shopfloor.Database.Initializers
{
    public class DatabaseInitializerFactory
    {
        private readonly IDbConnection _connection;
        private readonly string? _databaseType;

        public DatabaseInitializerFactory(IServiceProvider service, IDbConnection connection)
        {
            _connection = connection;
            _databaseType = service.GetRequiredService<DatabaseConfiguration>().Type;
        }

        public IDatabaseInitializer CreateInitializer()
        {
            return _databaseType switch
            {
                "SQLite" => new SQLiteDatabaseInitializer(_connection),
                _ => throw new InvalidOperationException("Invalid or unsupported database type."),
            };
        }
    }
}
