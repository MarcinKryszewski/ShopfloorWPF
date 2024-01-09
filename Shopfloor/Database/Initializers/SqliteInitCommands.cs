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
                user_name TEXT,
                user_surname TEXT,
                image_path TEXT,
                PRIMARY KEY(id AUTOINCREMENT)
            )";

        private const string _rolesSQLCommand = @"
            CREATE TABLE IF NOT EXISTS roles (
                id INTEGER,
                role_name TEXT,
                role_value INTEGER,
                PRIMARY KEY(id AUTOINCREMENT)
            )";

        private const string _roleUserSQLCommand = @"
            CREATE TABLE IF NOT EXISTS roles_users (
                role INTEGER,
                user INTEGER,                
                PRIMARY KEY(role, user),
                FOREIGN KEY(user) REFERENCES users(Id),
                FOREIGN KEY(role) REFERENCES roles(Id)
            )";

        public SqliteInitCommands()
        {
            InitCommands = new List<string>
            {
                _partsTypesSQLCommand,
                _userSQLCommand,
                _rolesSQLCommand,
                _roleUserSQLCommand
            };
        }
    }
}
