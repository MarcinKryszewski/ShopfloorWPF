using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.MachineModel
{
    internal sealed class MachineProvider : IProvider<Machine>
    {
        private const string _createSQL = @"
            INSERT INTO machines (machine_name, machine_number, parent)
            VALUES (@Name, @Number, @Parent)
            ";
        private const string _deleteSQL = @"
            DELETE
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
        private const string _updateSQL = @"
            UPDATE machines
            SET
                machine_name = @Name,
                machine_number = @Number,
                parent = @Parent,
                active = @Active
            WHERE id = @Id
            ";
        private readonly DatabaseConnectionFactory _database;
        public MachineProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<int> Create(Machine item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Name = item.Name,
                Number = item.Number,
                Parent = item.ParentId,
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            return 0;
        }
        public async Task Delete(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
            };
            await connection.ExecuteAsync(_deleteSQL, parameters);
        }
        public async Task<IEnumerable<Machine>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<MachineDto> machineDTOs = await connection.QueryAsync<MachineDto>(_getAllSQL);
            return machineDTOs.Select(ToModel);
        }
        public async Task<Machine> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
            };
            MachineDto? machineDTO = await connection.QuerySingleAsync<MachineDto>(_getOneSQL, parameters);
            return ToModel(machineDTO);
        }
        public async Task Update(Machine item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                Name = item.Name,
                Number = item.Number,
                Parent = item.ParentId,
                Active = item.IsActive,
            };
            await connection.ExecuteAsync(_updateSQL, parameters);
        }
        private static Machine ToModel(MachineDto item)
        {
            return new Machine()
            {
                Id = (int)item.Id!,
                Name = item.Name,
                Number = item.Number,
                SapNumber = item.SapNumber,
                ParentId = item.Parent,
                IsActive = item.Active,
            };
        }
    }
}