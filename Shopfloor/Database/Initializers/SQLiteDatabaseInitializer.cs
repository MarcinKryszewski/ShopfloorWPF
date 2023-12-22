﻿using Dapper;
using Shopfloor.Database.SQLite;
using System.Data;

namespace Shopfloor.Database.Initializers
{
    public class SQLiteDatabaseInitializer : IDatabaseInitializer
    {
        private readonly IDbConnection _connection;

        public SQLiteDatabaseInitializer(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Initialize()
        {
            using (_connection)
            {
                _connection.Open();
                CreateDatabase();
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
