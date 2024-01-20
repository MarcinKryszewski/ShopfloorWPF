using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Common;

namespace Shopfloor.Database
{
    public class DatabaseConnectionFactory : IDisposable
    {
        #region Fields

        private readonly string _databaseType;
        private readonly IConfiguration _configuration;
        private DbConnection? _connection;

        #endregion Fields

        #region Properties

        public string DatabaseType => _databaseType;

        #endregion Properties

        #region Constructors

        public DatabaseConnectionFactory(IServiceProvider configurationServices)
        {
            _configuration = configurationServices.GetRequiredService<IConfiguration>();
            _databaseType = _configuration["DatabaseType"] ?? "";
        }

        #endregion Constructors

        #region Methods

        public DbConnection Connect()
        {
            string connectionString;

            switch (_databaseType)
            {
                case "SQLite":
                    connectionString = _configuration.GetConnectionString("SQLiteConnection") ?? "";
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

        #endregion Methods
    }
}