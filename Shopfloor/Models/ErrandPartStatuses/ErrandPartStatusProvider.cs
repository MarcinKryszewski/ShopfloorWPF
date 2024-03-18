using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartStatusModel
{
    internal sealed class ErrandPartStatusProvider : IProvider<ErrandPartStatus>
    {
        private readonly DatabaseConnectionFactory _database;
        private const string _dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private const string _updateSQL = @"
            UPDATE errand_part_statuses
            SET
                errand_part_id = @ErrandPartId,
                errand_status_name = @StatusName,
                create_date = @CreatedDate,
                completed_by_id = @CompletedById,
                comment = @Comment,
                reason = @Reason,
                confirmed = @Confirmed
            WHERE id = @Id
        ;";
        private const string _getByErrandIdSQL = @"
            SELECT
                id as Id,
                errand_part_id AS ErrandPartId,
                errand_status_name AS StatusName,
                create_date AS CreatedDate,
                completed_by_id AS CompletedById,
                comment AS Comment,
                reason AS Reason,
                confirmed AS Confirmed
            FROM errand_part_statuses
            WHERE errand_part_id = @ErrandPartId
            ;";
        private const string _getAllSQL = @"
            SELECT
                id as Id,
                errand_part_id AS ErrandPartId,
                errand_status_name AS StatusName,
                create_date AS CreatedDate,
                completed_by_id AS CompletedById,
                comment AS Comment,
                reason AS Reason,
                confirmed AS Confirmed
            FROM errand_part_statuses
        ;";
        private const string _createSQL = @"
            INSERT INTO errand_part_statuses (errand_part_id, errand_status_name, create_date, comment, reason, confirmed)
            VALUES (@ErrandPartId, @StatusName, @CreatedDate, @Comment, @Reason, @Confirmed);";
        private const string _confirmSQL = @"
            UPDATE errand_part_statuses
            SET confirmed = 1
            WHERE id = @Id
        ";
        private const string _abortSQL = @"
            UPDATE errand_part_statuses
            SET confirmed = 0
            WHERE id = @Id
        ";
        private const string _setCommentSQL = @"
            UPDATE errand_part_statuses
            SET
                comment = @Comment,
                completed_by_id = @User,
                completed = 1,
                completed_date = @CompletedDate
            WHERE id = @Id
        ";
        public ErrandPartStatusProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        public async Task<int> Create(ErrandPartStatus item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                ErrandPartId = item.ErrandPartId,
                StatusName = item.StatusName,
                CreatedDate = item.CreatedDate.ToString(_dateTimeFormat),
                Comment = item.Comment,
                Reason = item.Reason,
                Confirmed = item.Confirmed
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            string lastIdSQL = "SELECT last_insert_rowid()";
            return connection.Query<int>(lastIdSQL).Single();
        }
        public Task Delete(int id) => throw new NotImplementedException();
        public async Task<IEnumerable<ErrandPartStatus>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<ErrandPartStatusDTO> ErrandPartStatusDTOs = await connection.QueryAsync<ErrandPartStatusDTO>(_getAllSQL);
            return ErrandPartStatusDTOs.Select(ToModel);
        }
        public Task<ErrandPartStatus> GetById(int id) => throw new NotImplementedException();
        public async Task<IEnumerable<ErrandPartStatus>> GetAllForErrand(int errandId)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = errandId
            };
            IEnumerable<ErrandPartStatusDTO> ErrandPartStatusDTOs = await connection.QueryAsync<ErrandPartStatusDTO>(_getByErrandIdSQL, parameters);
            return ErrandPartStatusDTOs.Select(ToModel);
        }
        public async Task Update(ErrandPartStatus item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                ErrandPartId = item.ErrandPartId,
                StatusName = item.StatusName,
                CreatedDate = item.CreatedDate.ToString(_dateTimeFormat),
                CompletedById = item.CompletedById,
                Comment = item.Comment,
                Reason = item.Reason,
                Confirmed = item.Confirmed
            };
            await connection.ExecuteAsync(_updateSQL, parameters);
        }
        public async Task Confirm(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            await connection.ExecuteAsync(_confirmSQL, parameters);
        }
        public async Task Abort(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            await connection.ExecuteAsync(_abortSQL, parameters);
        }
        public async Task ConfirmStatus(int id, string? comment, int userId)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
                Comment = comment,
                User = userId,
                CompletedDate = DateTime.Now.ToString(_dateTimeFormat)
            };
            await connection.ExecuteAsync(_setCommentSQL, parameters);
        }
        private static ErrandPartStatus ToModel(ErrandPartStatusDTO item)
        {
            return new ErrandPartStatus(item.StatusName)
            {
                Id = item.Id,
                ErrandPartId = item.ErrandPartId,
                CreatedDate = item.CreatedDate,
                CompletedById = item.CompletedById,
                Comment = item.Comment,
                Reason = item.Reason,
                Confirmed = item.Confirmed,
            };
        }
    }
}