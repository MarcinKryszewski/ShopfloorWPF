using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandModel
{
    internal sealed class ErrandProvider : IProvider<Errand>
    {
        private readonly DatabaseConnectionFactory _database;
        #region SQLCommands
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
            )";
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
        private const string _updateSQL = @"
            UPDATE errands
            SET
                created_date = @CeatedDate,
                created_by_id = @CreatedById,
                owner_id = @OwnerId,
                priority = @Priority,
                machine_id = @MachineId,
                errand_type_id = @ErrandTypeId,
                description = @Description,
                sap_number = @SapNumber,
                expected_date = @ExpectedDate
            WHERE id = @Id
            ";
        private const string _deleteSQL = @"
            DELETE
            FROM errands
            WHERE id = @Id
            ";
        #endregion SQLCommands
        public ErrandProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        #region CRUD
        public async Task<int> Create(Errand item)
        {
            CultureInfo culture = new("en-GB");
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                CeatedDate = item.CreatedDate.ToString(culture),
                CreatedById = item.CreatedById,
                OwnerId = item.OwnerId,
                Priority = item.Priority,
                MachineId = item.MachineId,
                ErrandTypeId = item.ErrandTypeId,
                Description = item.Description,
                SapNumber = item.SapNumber,
                ExpectedDate = item.ExpectedDate?.ToString("d", culture)
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            string lastIdSQL = "SELECT last_insert_rowid()";
            return connection.Query<int>(lastIdSQL).Single();
        }
        public async Task<IEnumerable<Errand>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<ErrandDTO> errandDTOs = await connection.QueryAsync<ErrandDTO>(_getAllSQL);
            return errandDTOs.Select(ToErrand);
        }
        public async Task<Errand> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            ErrandDTO? errandDTO = await connection.QuerySingleAsync<ErrandDTO>(_getOneSQL, parameters);
            return ToErrand(errandDTO);
        }
        public async Task Update(Errand item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                CeatedDate = item.CreatedDate,
                CreatedById = item.CreatedById,
                OwnerId = item.OwnerId,
                Priority = item.Priority,
                MachineId = item.MachineId,
                ErrandTypeId = item.ErrandTypeId,
                Description = item.Description,
                SapNumber = item.SapNumber,
                ExpectedDate = item.ExpectedDate
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
        private static Errand ToErrand(ErrandDTO item)
        {
            return new Errand(
                item.Id,
                item.CreatedDate,
                item.CreatedById,
                item.MachineId,
                item.ErrandTypeId,
                item.Description,
                item.SapNumber,
                item.ExpectedDate,
                item.OwnerId,
                item.Priority
            );
        }
    }
}