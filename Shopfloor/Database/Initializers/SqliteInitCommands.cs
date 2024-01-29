using System;
using System.Collections.Generic;
using System.Reflection;

namespace Shopfloor.Database.SQLite
{
    public sealed class SqliteInitCommands
    {
        public List<string> InitCommands { get; }
        private const string _parts_types_SQLCommand = @"
            CREATE TABLE IF NOT EXISTS parts_types (
                id INTEGER,
                part_type_name TEXT,
                PRIMARY KEY(id AUTOINCREMENT)
            )";
        private const string _user_SQLCommand = @"
            CREATE TABLE IF NOT EXISTS users (
                id INTEGER,
                username TEXT,
                user_name TEXT,
                user_surname TEXT,
                image_path TEXT,
                active INTEGER DEFAULT 1,
                PRIMARY KEY(id AUTOINCREMENT)
            )";
        private const string _roles_SQLCommand = @"
            CREATE TABLE IF NOT EXISTS roles (
                id INTEGER,
                role_name TEXT,
                role_value INTEGER,
                PRIMARY KEY(id AUTOINCREMENT)
            )";
        private const string _roles_users_SQLCommand = @"
            CREATE TABLE IF NOT EXISTS roles_users (
                role_id INTEGER,
                user_id INTEGER,
                PRIMARY KEY(role_id, user_id),
                FOREIGN KEY(user_id) REFERENCES users(Id),
                FOREIGN KEY(role_id) REFERENCES roles(Id)
            )";
        private const string _initAdmin_SQLCommand = @"
            BEGIN TRANSACTION;
                INSERT INTO users (username, user_name, user_surname, image_path) VALUES ('@dm1n', 'Admin', 'Admin', '');
                INSERT INTO users (username, user_name, user_surname, image_path) VALUES ('marcin', 'Marcin', 'Kry', '');
                INSERT INTO users (username, user_name, user_surname, image_path) VALUES ('kryszm02', 'Marcin', 'Kry', '');
            COMMIT;
            ";
        private const string _initRoles_SQLCommand = @"
            BEGIN TRANSACTION;
                INSERT INTO roles (role_name, role_value) VALUES ('admin', 777);
                INSERT INTO roles (role_name, role_value) VALUES ('user', 568);
                INSERT INTO roles (role_name, role_value) VALUES ('plannist', 460);
                INSERT INTO roles (role_name, role_value) VALUES ('manager', 205);
            COMMIT;
            ";
        private const string _initAdminRoles_SQLCommand = @"
            BEGIN TRANSACTION;
                INSERT INTO roles_users (role_id, user_id) VALUES (1,1);
                INSERT INTO roles_users (role_id, user_id) VALUES (2,1);
                INSERT INTO roles_users (role_id, user_id) VALUES (3,1);
                INSERT INTO roles_users (role_id, user_id) VALUES (4,1);
                INSERT INTO roles_users (role_id, user_id) VALUES (1,2);
                INSERT INTO roles_users (role_id, user_id) VALUES (2,2);
                INSERT INTO roles_users (role_id, user_id) VALUES (3,2);
                INSERT INTO roles_users (role_id, user_id) VALUES (4,2);
                INSERT INTO roles_users (role_id, user_id) VALUES (1,3);
                INSERT INTO roles_users (role_id, user_id) VALUES (2,3);
                INSERT INTO roles_users (role_id, user_id) VALUES (3,3);
                INSERT INTO roles_users (role_id, user_id) VALUES (4,3);
            COMMIT;
            ";
        private const string _machines_SQLCommand = @"
            CREATE TABLE IF NOT EXISTS machines (
                id INTEGER,
                machine_name TEXT,
                machine_number TEXT,
                sap_number TEXT,
                parent INTEGER,
                active INTEGER DEFAULT 1,
                PRIMARY KEY(id)
            )";
        private const string _suppliers_SQLCommand = @"
            CREATE TABLE IF NOT EXISTS suppliers (
                id INTEGER,
                name TEXT,
                active INTEGER DEFAULT 1,
                PRIMARY KEY(id)
            )";
        private const string _parts_SQLCommand = @"
            CREATE TABLE parts (
                id INTEGER,
                name_pl TEXT,
                name_original TEXT,
                type_id INTEGER,
                indeks INTEGER,
                number TEXT,
                details TEXT,
                producer_id INTEGER,
                supplier_id INTEGER,
                PRIMARY KEY(id),
                FOREIGN KEY(type_id) REFERENCES parts_types(id),
                FOREIGN KEY(supplier_id) REFERENCES suppliers(id),
                FOREIGN KEY(producer_id) REFERENCES suppliers(id)
            )";
        private const string _machines_parts_SQLCommand = @"
            CREATE TABLE machines_parts (
                machine_id INTEGER,
                part_id INTEGER,
                amount REAL,
                unit TEXT,
                FOREIGN KEY(machine_id) REFERENCES machines(id),
                FOREIGN KEY(part_id) REFERENCES parts,
                PRIMARY KEY(machine_id,part_id)
            )";
        private const string _errands_types_SQLCommand = @"
            CREATE TABLE errand_types (
                id INTEGER,
                name TEXT,
                description TEXT,
                PRIMARY KEY(id)
            )";
        private const string _initPart_types_SQLCommand = @"
            BEGIN TRANSACTION;
                INSERT INTO errand_types (name, description) VALUES ('Awaria', 'Zadania związane z awariami na liniach');
                INSERT INTO errand_types (name, description) VALUES ('CILT', 'Cykliczne zadania związane z utrzymaniem maszyn');
                INSERT INTO errand_types (name, description) VALUES ('DCS', 'Zadania zaplanowane do wykonania wynikające np. z TAGów czy spotkań DCS');
                INSERT INTO errand_types (name, description) VALUES ('Remont', 'Zadania do wykonania podczas corocznych remontów');
                INSERT INTO errand_types (name, description) VALUES ('Warsztat', 'Zadania związane z działaniem warsztatu, np. narzędzia, śruby czy materiały eksploatacyjne na warsztacie');
            COMMIT;
            ";
        private const string _errand_statuses_SQLCommand = @"
        CREATE TABLE errand_statuses (
            id INTEGER,
            description TEXT,
            PRIMARY KEY(id)
        )";
        private const string _errands_SQLCommand = @"
            CREATE TABLE errands (
                id INTEGER,
                created_date TEXT,
                created_by_id INTEGER,
                owner_id INTEGER,
                priority TEXT,
                machine_id INTEGER,
                errand_type_id INTEGER,
                description TEXT,
                sap_number TEXT,
                expected_date TEXT,
                FOREIGN KEY(machine_id) REFERENCES machines(id),
                FOREIGN KEY(errand_type_id) REFERENCES errand_types(id),
                FOREIGN KEY(created_by_id) REFERENCES users(id)
                PRIMARY KEY(id)
            )";
        private const string _errands_errand_statusesSQLCommand = @"
            CREATE TABLE errands_errand_statuses (
                errand_id INTEGER,
                errand_status_id INTEGER,
                set_date TEXT,
                PRIMARY KEY(errand_id, errand_status_id),
                FOREIGN KEY(errand_id) REFERENCES errands(id),
                FOREIGN KEY(errand_status_id) REFERENCES errand_statuses(id)
            )";
        private const string _parts_errands_SQLCommand = @"
        CREATE TABLE parts_errands (
            part_id INTEGER,
            errand_id INTEGER,
            amount INTEGER,
            status TEXT,
            FOREIGN KEY(errand_id) REFERENCES errands(id),
            FOREIGN KEY(part_id) REFERENCES parts(id)
        )";
        public SqliteInitCommands()
        {
            InitCommands =
            [
                _parts_types_SQLCommand,
                _user_SQLCommand,
                _roles_SQLCommand,
                _roles_users_SQLCommand,
                _machines_SQLCommand,
                _suppliers_SQLCommand,
                _parts_SQLCommand,
                _machines_parts_SQLCommand,
                _errands_types_SQLCommand,

                _errand_statuses_SQLCommand,
                _errands_SQLCommand,
                _errands_errand_statusesSQLCommand,
                _parts_errands_SQLCommand,


                _initAdmin_SQLCommand,
                _initRoles_SQLCommand,
                _initAdminRoles_SQLCommand,
                _initPart_types_SQLCommand
            ];
        }
    }
}