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
                active INTEGER DEFAULT 1,
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
        private const string _initAdminSQLCommand = @"
            INSERT INTO users (username, user_name, user_surname, image_path)
            VALUES ('@dm1n', 'Admin', 'Admin', '')
            ";
        private const string _initRolesSQLCommand = @"
            BEGIN TRANSACTION;
            INSERT INTO roles (role_name, role_value) VALUES ('admin', 777);
            INSERT INTO roles (role_name, role_value) VALUES ('user', 568);
            INSERT INTO roles (role_name, role_value) VALUES ('plannist', 460);
            INSERT INTO roles (role_name, role_value) VALUES ('manager', 205);
            COMMIT;
            ";
        private const string _initAdminRolesSQLCommand = @"
            BEGIN TRANSACTION;
            INSERT INTO roles_users (role, user) VALUES (1,1);
            INSERT INTO roles_users (role, user) VALUES (2,1);
            INSERT INTO roles_users (role, user) VALUES (3,1);
            INSERT INTO roles_users (role, user) VALUES (4,1);
            COMMIT;
            ";
        private const string _machinesSQLCommand = @"
            CREATE TABLE IF NOT EXISTS machines (
                id INTEGER,
                machine_name TEXT,
                machine_number TEXT,
                parent INTEGER,
                active INTEGER DEFAULT 1,
                PRIMARY KEY(id)
            )";
        private const string _parts_typesSQLCommand = @"
            CREATE TABLE IF NOT EXISTS parts_types (
                id INTEGER,
                part_type_name TEXT,
                PRIMARY KEY(id)
            )";
        private const string _suppliersSQLCommand = @"
            CREATE TABLE IF NOT EXISTS suppliers (
                id INTEGER,
                name TEXT,
                active INTEGER DEFAULT 1,
                PRIMARY KEY(id)
            )";
        private const string _partsSQLCommand = @"
            CREATE TABLE parts (
                id INTEGER,
                name_pl TEXT,
                name_original TEXT,
                type INTEGER,
                indeks INTEGER,
                number TEXT,
                details TEXT,
                producer INTEGER,
                supplier INTEGER,
                PRIMARY KEY(id),
                FOREIGN KEY(type) REFERENCES parts_types(id),
                FOREIGN KEY(supplier) REFERENCES suppliers(id),
                FOREIGN KEY(producer) REFERENCES suppliers(id)
            )";
        private const string _machines_partsSQLCommand = @"
            CREATE TABLE machines_parts (
                machine INTEGER,
                part INTEGER,
                amount REAL,
                unit TEXT,                
                FOREIGN KEY(machine) REFERENCES machines(id),                
                FOREIGN KEY(part) REFERENCES parts,
                PRIMARY KEY(machine,part)
            )";

        public SqliteInitCommands()
        {
            InitCommands = new List<string>
            {
                _partsTypesSQLCommand,
                _userSQLCommand,
                _rolesSQLCommand,
                _roleUserSQLCommand,
                _initAdminSQLCommand,
                _initRolesSQLCommand,
                _initAdminRolesSQLCommand,
                _machinesSQLCommand,
                _parts_typesSQLCommand,
                _suppliersSQLCommand,
                _partsSQLCommand,
                _machines_partsSQLCommand
            };
        }
    }
}
