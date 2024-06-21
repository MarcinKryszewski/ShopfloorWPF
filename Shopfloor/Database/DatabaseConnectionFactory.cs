using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;

namespace Shopfloor.Database
{
    internal sealed class DatabaseConnectionFactory : IDisposable
    {
        private const string _databaseTypeKey = "DatabaseType";
        private const string _sqlliteConnectionKey = "SQLiteConnection";
        private const string _sqliteDbName = "SQLite";
        private const string _invalidDbError = "Invalid or unsupported database type.";

        private readonly string _databaseType;
        private readonly IConfiguration _configuration;
        private DbConnection? _connection;
        public string DatabaseType => _databaseType;
        public DatabaseConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _databaseType = _configuration[_databaseTypeKey] ?? string.Empty;
        }
        public DbConnection Connect()
        {
            string connectionString;

            switch (_databaseType)
            {
                case _sqliteDbName:
                    connectionString = _configuration.GetConnectionString(_sqlliteConnectionKey) ?? string.Empty;
                    _connection = new SqliteConnection(connectionString);
                    break;

                default:
                    throw new InvalidOperationException(_invalidDbError);
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