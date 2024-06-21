using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandStatusModel
{
    internal sealed class ErrandStatusProvider : IProvider<ErrandStatus>
    {
        private readonly DatabaseConnectionFactory _database;
        private const string _dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private const string _updateSQL = @"
            UPDATE errand_statuses
            SET
                errand_id = @ErrandId,
                errand_status_name = @StatusName,
                set_date = @SetDate,
                comment = @Comment,
                reason = @Reason,
            WHERE id = @Id
        ;";
        private const string _getByErrandIdSQL = @"
            SELECT
                id as Id,
                errand_id AS ErrandId,
                errand_status_name AS StatusName,
                set_date AS SetDate,
                comment AS Comment,
                reason AS Reason
            FROM errand_statuses
            WHERE errand_id = @Id
            ;";
        private const string _getAllSQL = @"
            SELECT
                id as Id,
                errand_id AS ErrandId,
                errand_status_name AS StatusName,
                set_date AS SetDate,
                comment AS Comment,
                reason AS Reason
            FROM errand_statuses
        ;";
        private const string _createSQL = @"
            INSERT INTO errand_statuses (
                errand_id,
                errand_status_name,
                set_date,
                comment,
                reason
            )
            VALUES (
                @ErrandId,
                @StatusName,
                @SetDate,
                @Comment,
                @Reason
            );";
        public ErrandStatusProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        public async Task<int> Create(ErrandStatus item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                ErrandId = item.ErrandId,
                StatusName = item.StatusName,
                SetDate = item.SetDate.ToString(_dateTimeFormat),
                Comment = item.Comment,
                Reason = item.Reason
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            string lastIdSQL = "SELECT last_insert_rowid()";
            return connection.Query<int>(lastIdSQL).Single();
        }
        public Task Delete(int id) => throw new System.NotImplementedException();
        public async Task<IEnumerable<ErrandStatus>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<ErrandStatusDTO> errandStatusDTOs = await connection.QueryAsync<ErrandStatusDTO>(_getAllSQL);
            return errandStatusDTOs.Select(ToModel);
        }
        public Task<ErrandStatus> GetById(int id) => throw new System.NotImplementedException();
        public async Task<IEnumerable<ErrandStatus>> GetAllForErrand(int errandId)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = errandId
            };
            IEnumerable<ErrandStatusDTO> errandStatusDTOs = await connection.QueryAsync<ErrandStatusDTO>(_getByErrandIdSQL, parameters);
            return errandStatusDTOs.Select(ToModel);
        }
        public async Task Update(ErrandStatus item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                ErrandId = item.ErrandId,
                StatusName = item.StatusName,
                SetDate = item.SetDate.ToString(_dateTimeFormat),
                Comment = item.Comment,
                Reason = item.Reason
            };
            await connection.ExecuteAsync(_updateSQL, parameters);
        }
        private static ErrandStatus ToModel(ErrandStatusDTO item)
        {
            return new ErrandStatus()
            {
                Id = item.Id,
                ErrandId = item.ErrandId,
                StatusName = item.StatusName,
                SetDate = item.SetDate,
                Comment = item.Comment,
                Reason = item.Reason,
            };
        }
    }
}