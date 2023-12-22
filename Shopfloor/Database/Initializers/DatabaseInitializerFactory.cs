using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace PrzegladyRemonty.Database.Initializers
{
    public class DatabaseInitializerFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;
        private readonly string? _databaseType;

        public DatabaseInitializerFactory(IConfiguration configuration, IDbConnection connection)
        {
            _configuration = configuration;
            _connection = connection;
            _databaseType = _configuration["DatabaseType"];
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
