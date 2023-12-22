using System.Collections.Generic;

namespace PrzegladyRemonty.Database.SQLite
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
        private const string _areaSQLCommand = @"
            CREATE TABLE IF NOT EXISTS area (
                Id INTEGER,
                Name INTEGER,
                Active INTEGER,
                Line INTEGER,
                PRIMARY KEY(Id AUTOINCREMENT)
            )";
        private const string _transporterTypeSQLCommand = @"
            CREATE TABLE IF NOT EXISTS transporterType (
                Id INTEGER, 
                Name TEXT,
                PRIMARY KEY(Id AUTOINCREMENT)
            );";
        private const string _transporterSQLCommand = @"
            CREATE TABLE IF NOT EXISTS transporter (
                Id INTEGER,
                Name TEXT,
                Active INTEGER,
                Area INTEGER,
                LastMaintenance TEXT,
                TransporterType INTEGER,
                Status TEXT,
                FOREIGN KEY(Area) REFERENCES area (Id),
                FOREIGN KEY(TransporterType) REFERENCES transporterType(Id),
                PRIMARY KEY(Id AUTOINCREMENT)
            )";
        private const string _actionCategorySQLCommand = @"
            CREATE TABLE IF NOT EXISTS actionCategory (
                Id INTEGER,
                Name TEXT,
                PRIMARY KEY(Id AUTOINCREMENT)
            )";
        private const string _transporterActionSQLCommand = @"
            CREATE TABLE IF NOT EXISTS transporterAction (
                Id INTEGER,
                Transporter INTEGER,
                MaintenanceAction INTEGER,
                Frequency INTEGER,
                FrequencyUnit TEXT,
                Status TEXT,
                FOREIGN KEY(MaintenanceAction) REFERENCES actionCategory(Id),
                FOREIGN KEY(Transporter) REFERENCES transporter(Id),
                PRIMARY KEY(Id AUTOINCREMENT)
            )";
        private const string _permissionSQLCommand = @"
            CREATE TABLE IF NOT EXISTS permission (
                Id INTEGER,
                Name TEXT,
                PermissionValue INTEGER,
                PRIMARY KEY(Id AUTOINCREMENT)
            )";
        private const string _personSQLCommand = @"
            CREATE TABLE IF NOT EXISTS person (
                Id INTEGER,
                Login TEXT,
                Name TEXT,
                Surname TEXT,
                Active INTEGER,
                PRIMARY KEY(Id AUTOINCREMENT)
            )";
        private const string _personPermissionSQLCommand = @"
            CREATE TABLE IF NOT EXISTS personPermission (
                Id INTEGER,
                Person INTEGER,
                Permission INTEGER,
                PRIMARY KEY(Id AUTOINCREMENT),
                FOREIGN KEY(Person) REFERENCES person(Id),
                FOREIGN KEY(Permission) REFERENCES permission(Id)
            )";
        private const string _maintenanceSQLCommand = @"
            CREATE TABLE IF NOT EXISTS maintenance (
                Id INTEGER,
                MaintenanceDate TEXT,
                Mechanic INTEGER,
                MaintenanceAction INTEGER,
                Completed INTEGER,
                Description TEXT,
                FOREIGN KEY(Mechanic) REFERENCES person(Id),
                FOREIGN KEY(MaintenanceAction) REFERENCES transporterAction(Id),
                PRIMARY KEY(Id AUTOINCREMENT)
            )";
        private const string _workOrderSQLCommand = @"
            CREATE TABLE IF NOT EXISTS workOrder (
                Id INTEGER,
                Created TEXT,
                CreatedBy INTEGER,
                Status TEXT,
                PRIMARY KEY(Id AUTOINCREMENT)
            )";
        private const string _workOrderMaintenanceSQLCommand = @"
            CREATE TABLE IF NOT EXISTS workOrderMaintenance (
                Id INTEGER,
                WorkOrder INTEGER,
                Maintenance INTEGER,
                FOREIGN KEY(WorkOrder) REFERENCES workOrder(Id),
                FOREIGN KEY(Maintenance) REFERENCES maintenance(Id),
                PRIMARY KEY(Id AUTOINCREMENT)
            )";
        private const string _partSQLCommand = @"
            CREATE TABLE IF NOT EXISTS part (
                Id INTEGER,
                Name TEXT,
                Producent TEXT,
                ProducentNumber TEXT,
                PRIMARY KEY(Id AUTOINCREMENT)
            )";
        private const string _transporterPartSQLCommand = @"
            CREATE TABLE IF NOT EXISTS transporterPart (
                Id INTEGER,
                Transporter INTEGER,
                Part INTEGER,
                Amount INTEGER,
                Unit TEXT,
                FOREIGN KEY(Part) REFERENCES part(Id),
                PRIMARY KEY(Id AUTOINCREMENT),
                FOREIGN KEY(Transporter) REFERENCES transporter(Id)
            )";

        public SqliteInitCommands()
        {
            InitCommands = new List<string>
            {
                _lineSQLCommand,
                _areaSQLCommand,
                _transporterSQLCommand,
                _transporterTypeSQLCommand,
                _actionCategorySQLCommand,
                _transporterActionSQLCommand,
                _permissionSQLCommand,
                _personSQLCommand,
                _personPermissionSQLCommand,
                _maintenanceSQLCommand,
                _workOrderSQLCommand,
                _workOrderMaintenanceSQLCommand,
                _partSQLCommand,
                _transporterPartSQLCommand
            };
        }
    }
}
