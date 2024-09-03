using System.Collections.Generic;

namespace Shopfloor.Database.SQLite
{
    public sealed class SqliteInitCommands
    {
        public SqliteInitCommands()
        {
            InitCommands =
            [
            ];
        }
        public List<string> InitCommands { get; }
    }
}