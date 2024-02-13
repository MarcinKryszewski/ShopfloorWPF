using Dapper;
using Shopfloor.Database.SQLite;
using System.Data;
using System.IO;

namespace Shopfloor.Database.Initializers
{
    internal sealed class SQLiteDatabaseInitializer : IDatabaseInitializer
    {
        private readonly IDbConnection _connection;
        private readonly string _databasePath;

        public SQLiteDatabaseInitializer(IDbConnection connection, string databasePath)
        {
            _connection = connection;
            _databasePath = databasePath;
        }

        public void Initialize()
        {
            using (_connection)
            {
                if (!File.Exists(_databasePath)) CreateDatabase();
                _connection.Open();
            }
        }

        public void CreateDatabase()
        {
            foreach (string command in new SqliteInitCommands().InitCommands)
            {
                _connection.Execute(command);
            }
        }
    }
}