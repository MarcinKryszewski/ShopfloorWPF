using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.MachinePartModel
{
    internal sealed class MachinePartProvider : IProvider<MachinePart>
    {
        private const string _createSQL = @"
            INSERT INTO machines_parts (machine_id, part_id, amount)
            VALUES (@Machine, @Part, @Amount)
        ";
        private const string _deleteSQL = @"
            DELETE
            FROM machines_parts
            WHERE
                machine_id = @Machine AND
                part_id = @Part
        ";
        private const string _getAllSQL = @"
            SELECT
                machine_id AS MachineId,
                part_id AS PartId,
                amount AS Amount
            FROM machines_parts
            ";
        private const string _getOneSQL = @"
            SELECT
                machine_id AS MachineId,
                part_id AS PartId,
                amount AS Amount
            FROM machines_parts
            WHERE
                machine_id = @Machine AND
                part_id = @Part
        ";
        private readonly DatabaseConnectionFactory _database;
        public MachinePartProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        public async Task<int> Create(MachinePart item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Machine = item.MachineId,
                Part = item.PartId,
                Amount = item.Amount,
            };
            await connection.ExecuteAsync(_createSQL, parameters);
            return 0;
        }
        public async Task Delete(int machineId, int partId)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Machine = machineId,
                Part = partId,
            };
            await connection.ExecuteAsync(_deleteSQL, parameters);
        }
        public Task Delete(int id) => throw new System.NotImplementedException();
        public async Task<IEnumerable<MachinePart>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<MachinePartDto> machineDTOs = await connection.QueryAsync<MachinePartDto>(_getAllSQL);
            return machineDTOs.Select(ToModel);
        }
        public async Task<MachinePart> GetById(int machineId, int partId)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Machine = machineId,
                Part = partId,
            };
            MachinePartDto? machinePartDTO = await connection.QuerySingleAsync<MachinePartDto>(_getOneSQL, parameters);
            return ToModel(machinePartDTO);
        }
        public Task<MachinePart> GetById(int id) => throw new System.NotImplementedException();
        public Task Update(MachinePart item) => throw new System.NotImplementedException();
        private static MachinePart ToModel(MachinePartDto item)
        {
            return new MachinePart()
            {
                Amount = item.Amount,
                PartId = (int)item.PartId!,
                MachineId = (int)item.MachineId!,
            };
        }
    }
}