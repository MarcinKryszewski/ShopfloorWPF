using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel
{
    internal sealed class ErrandPartProvider : IProvider<ErrandPart>
    {
        private readonly DatabaseConnectionFactory _database;
        #region SQLCommands
        private const string _createSQL = @"
            INSERT INTO errands_parts (
                errand_id,
                part_id,
                amount,
                status
            )
            VALUES (
                @ErrandId,
                @PartId,
                @Amount,
                @Status
            )";
        private const string _getOneSQL = @"
            SELECT
                errand_id AS ErrandId,
                part_id AS PartId,
                amount AS Amount,
                status AS Status
            FROM errands_parts
            WHERE id = @Id
            ";
        private const string _getForErrandSQL = @"
            SELECT
                errand_id AS ErrandId,
                part_id AS PartId,
                amount AS Amount,
                status AS Status
            FROM errands_parts
            WHERE errand_id = @ErrandId
            ";
        private const string _getAllSQL = @"
            SELECT
                errand_id AS ErrandId,
                part_id AS PartId,
                amount AS Amount,
                status AS Status
            FROM errands_parts
            ";
        private const string _updateSQL = @"
            UPDATE errands_parts
            SET
                amount = @Amount,
                status = @Status
            WHERE errand_id = @ErrandId AND part_id = @PartId
            ";
        private const string _deleteSQL = @"
            UPDATE errands_parts
            SET status = @Status
            WHERE errand_id = @ErrandId AND part_id = @PartId
            ";
        #endregion SQLCommands
        public ErrandPartProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        #region CRUD
        public async Task<int> Create(ErrandPart item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                ErrandId = item.ErrandId,
                PartId = item.PartId,
                Amount = item.Amount,
                Status = item.Status
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            string lastIdSQL = "SELECT last_insert_rowid()";
            return connection.Query<int>(lastIdSQL).Single();
        }
        public async Task<IEnumerable<ErrandPart>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<ErrandPartDTO> errandPartDTOs = await connection.QueryAsync<ErrandPartDTO>(_getAllSQL);
            return errandPartDTOs.Select(ToErrandPart);
        }
        public async Task<IEnumerable<ErrandPart>> GetByErrandId(int errandId)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                ErrandId = errandId
            };
            IEnumerable<ErrandPartDTO> errandPartDTOs = await connection.QueryAsync<ErrandPartDTO>(_getForErrandSQL, parameters);
            return errandPartDTOs.Select(ToErrandPart);
        }
        public Task<ErrandPart> GetById(int id) => throw new NotImplementedException();
        public async Task Update(ErrandPart item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                ErrandId = item.ErrandId,
                PartId = item.PartId,
                Amount = item.Amount,
                Status = item.Status
            };
            await connection.ExecuteAsync(_updateSQL, parameters);
        }
        public Task Delete(int id) => throw new NotImplementedException();
        public async Task Delete(int errandId, int partId)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                ErrandId = errandId,
                PartId = partId,
                Status = ErrandPart.PartStatuses[6]
            };
            await connection.ExecuteAsync(_deleteSQL, parameters);
        }
        #endregion CRUD
        private static ErrandPart ToErrandPart(ErrandPartDTO item)
        {
            return new ErrandPart(
                item.ErrandId,
                item.PartId,
                item.Amount,
                item.Status
            );
        }
    }
}