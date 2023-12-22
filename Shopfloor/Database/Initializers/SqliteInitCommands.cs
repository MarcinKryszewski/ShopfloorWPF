using System.Collections.Generic;

namespace Shopfloor.Database.SQLite
{
    public class SqliteInitCommands
    {
        public List<string> InitCommands { get; }

        private const string _lineSQLCommand = @"
            CREATE TABLE IF NOT EXISTS line (
                Id INTEGER,
                Name TEXT,
                Active INTEGER,
                PRIMARY KEY(Id AUTOINCREMENT)
            )";

        public SqliteInitCommands()
        {
            InitCommands = new List<string>
            {
                _lineSQLCommand
            };
        }
    }
}
