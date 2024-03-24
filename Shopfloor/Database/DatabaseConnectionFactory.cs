using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;

namespace Shopfloor.Database
{
    internal sealed class DatabaseConnectionFactory : IDisposable
    {
        private readonly string _databaseType;
        private readonly IConfiguration _configuration;
        private DbConnection? _connection;
        public string DatabaseType => _databaseType;
        public DatabaseConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _databaseType = _configuration["DatabaseType"] ?? string.Empty;
        }
        public DbConnection Connect()
        {
            string connectionString;

            switch (_databaseType)
            {
                case "SQLite":
                    connectionString = _configuration.GetConnectionString("SQLiteConnection") ?? string.Empty;
                    _connection = new SqliteConnection(connectionString);
                    break;

                default:
                    throw new InvalidOperationException("Invalid or unsupported database type.");
            }
            return _connection;
        }
        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}