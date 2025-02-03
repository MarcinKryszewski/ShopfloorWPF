using System.Collections.Generic;

namespace Shopfloor.Database.Initializers
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