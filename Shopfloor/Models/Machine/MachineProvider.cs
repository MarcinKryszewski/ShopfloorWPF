using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.MachineModel
{
    internal sealed class MachineProvider : IProvider<Machine>
    {
        private readonly DatabaseConnectionFactory _database;
        #region SQLCommands
        private const string _createSQL = @"
            INSERT INTO machines (machine_name, machine_number, parent)
            VALUES (@Name, @Number, @Parent)
            ";
        private const string _getOneSQL = @"
            SELECT
                id AS Id,
                machine_name AS Name,
                machine_number AS Number,
                sap_number AS SapNumber,
                parent AS Parent,
                active AS Active
            FROM machines
            WHERE id = @Id
            ";
        private const string _getAllSQL = @"
            SELECT
                id AS Id,
                machine_name AS Name,
                machine_number AS Number,
                sap_number AS SapNumber,
                parent AS Parent,
                active AS Active
            FROM machines
            ";
        private const string _updateSQL = @"
            UPDATE machines
            SET
                machine_name = @Name,
                machine_number = @Number,
                parent = @Parent,
                active = @Active
            WHERE id = @Id
            ";
        private const string _deleteSQL = @"
            DELETE
            FROM machines
            WHERE id = @Id
            ";
        #endregion SQLCommands
        public MachineProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        #region CRUD
        public async Task<int> Create(Machine item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Name = item.Name,
                Number = item.Number,
                Parent = item.ParentId
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            return 0;
        }
        public async Task<IEnumerable<Machine>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<MachineDTO> machineDTOs = await connection.QueryAsync<MachineDTO>(_getAllSQL);
            return machineDTOs.Select(ToMachine);
        }
        public async Task<Machine> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            MachineDTO? machineDTO = await connection.QuerySingleAsync<MachineDTO>(_getOneSQL, parameters);
            return ToMachine(machineDTO);
        }
        public async Task UpdateAmount(Machine item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                Name = item.Name,
                Number = item.Number,
                Parent = item.ParentId,
                Active = item.IsActive
            };
            await connection.ExecuteAsync(_updateSQL, parameters);
        }
        public async Task Delete(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            await connection.ExecuteAsync(_deleteSQL, parameters);
        }
        #endregion CRUD
        private static Machine ToMachine(MachineDTO item)
        {
            return new Machine((int)item.Id!, item.Name, item.Number, item.SapNumber, item.Parent, item.Active);
        }
    }
}