using System.Collections.Generic;

namespace Shopfloor.Database.SQLite
{
    public class SqliteInitCommands
    {
        public List<string> InitCommands { get; }

        private const string _partsTypesSQLCommand = @"
            CREATE TABLE IF NOT EXISTS parts_types (
                id INTEGER,
                part_type_name TEXT,
                PRIMARY KEY(id AUTOINCREMENT)
            )";
        private const string _userSQLCommand = @"
            CREATE TABLE IF NOT EXISTS users (
                id INTEGER,
                username TEXT,
                PRIMARY KEY(id AUTOINCREMENT)
            )";


        public SqliteInitCommands()
        {
            InitCommands = new List<string>
            {
                _partsTypesSQLCommand,
                _userSQLCommand
            };
        }
    }
}
