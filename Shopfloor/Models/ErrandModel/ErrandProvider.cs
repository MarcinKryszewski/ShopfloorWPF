using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandModel
{
    internal sealed class ErrandProvider : IProvider<Errand>
    {
        private const string _createSQL = @"
            INSERT INTO errands (
                created_date,
                created_by_id,
                owner_id,
                priority,
                machine_id,
                errand_type_id,
                description,
                sap_number,
                expected_date
            )
            VALUES (
                @CeatedDate,
                @CreatedById,
                @OwnerId,
                @Priority,
                @MachineId,
                @ErrandTypeId,
                @Description,
                @SapNumber,
                @ExpectedDate
            );";
        private const string _dateFormat = "yyyy-MM-dd";
        private const string _dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private const string _deleteSQL = @"
            DELETE
            FROM errands
            WHERE id = @Id
            ";
        private const string _getAllSQL = @"
            SELECT
                id AS Id,
                created_date AS CreatedDate,
                created_by_id AS CreatedById,
                owner_id AS OwnerId,
                priority AS Priority,
                machine_id AS MachineId,
                errand_type_id AS ErrandTypeId,
                description AS Description,
                sap_number AS SapNumber,
                expected_date AS ExpectedDate
            FROM errands
            ";
        private const string _getOneSQL = @"
            SELECT
                id AS Id,
                created_date AS CreatedDate,
                created_by_id AS CreatedById,
                owner_id AS OwnerId,
                priority AS Priority,
                machine_id AS MachineId,
                errand_type_id AS ErrandTypeId,
                description AS Description,
                sap_number AS SapNumber,
                expected_date AS ExpectedDate
            FROM errands
            WHERE id = @Id
            ";
        private const string _updateSQL = @"
            UPDATE errands
            SET
                owner_id = @OwnerId,
                priority = @Priority,
                machine_id = @MachineId,
                errand_type_id = @ErrandTypeId,
                description = @Description,
                sap_number = @SapNumber,
                expected_date = @ExpectedDate
            WHERE id = @Id
            ";
        private readonly DatabaseConnectionFactory _database;
        public ErrandProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<int> Create(Errand item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                CeatedDate = item.CreatedDate.ToString(_dateTimeFormat),
                CreatedById = item.CreatedById,
                OwnerId = item.OwnerId,
                Priority = item.Priority,
                MachineId = item.MachineId,
                ErrandTypeId = item.TypeId,
                Description = item.Description,
                SapNumber = item.SapNumber,
                ExpectedDate = item.ExpectedDate?.ToString(_dateFormat),
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            string lastIdSQL = "SELECT last_insert_rowid()";
            return await connection.QueryFirstAsync<int>(lastIdSQL);
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
        public async Task<IEnumerable<Errand>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<ErrandDto> errandDTOs = await connection.QueryAsync<ErrandDto>(_getAllSQL);
            return errandDTOs.Select(ToErrand);
        }
        public async Task<Errand> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
            };
            ErrandDto? errandDTO = await connection.QuerySingleAsync<ErrandDto>(_getOneSQL, parameters);
            return ToErrand(errandDTO);
        }
        public async Task Update(Errand item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                OwnerId = item.OwnerId,
                Priority = item.Priority,
                MachineId = item.MachineId,
                ErrandTypeId = item.TypeId,
                Description = item.Description,
                SapNumber = item.SapNumber,
                ExpectedDate = item.ExpectedDate?.ToString(_dateFormat),
            };
            await connection.ExecuteAsync(_updateSQL, parameters);
        }
        private static Errand ToErrand(ErrandDto item)
        {
            return new Errand((int)item.Id!)
            {
                CreatedById = item.CreatedById,
                CreatedDate = item.CreatedDate!,
                Description = item.Description!,
                Priority = item.Priority,
                MachineId = item.MachineId,
                TypeId = item.ErrandTypeId,
                SapNumber = item.SapNumber,
                ExpectedDate = item.ExpectedDate,
                OwnerId = item.OwnerId,
            };
        }
    }
}