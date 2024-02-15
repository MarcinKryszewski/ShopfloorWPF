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
                ordered_by_id
            )
            VALUES (
                @ErrandId,
                @PartId,
                @Amount,
                @OrderedBy
            )";
        private const string _getOneSQL = @"
            SELECT
                errand_id AS ErrandId,
                part_id AS PartId,
                amount AS Amount,
                ordered_by_id as OrderedById
            FROM errands_parts
            WHERE id = @Id
            ";
        private const string _getForErrandSQL = @"
            SELECT
                errand_id AS ErrandId,
                part_id AS PartId,
                amount AS Amount,
                ordered_by_id as OrderedById
            FROM errands_parts
            WHERE errand_id = @ErrandId
            ";
        private const string _getAllSQL = @"
            SELECT
                id as Id,
                errand_id AS ErrandId,
                part_id AS PartId,
                amount AS Amount,
                ordered_by_id as OrderedById
            FROM errands_parts
            ";
        private const string _updateSQL = @"
            UPDATE errands_parts
            SET
                amount = @Amount
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
                OrderedBy = item.OrderedById
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
                Amount = item.Amount
            };
            await connection.ExecuteAsync(_updateSQL, parameters);
        }
        public Task Delete(int id) => throw new NotImplementedException();
        public Task Delete(int errandId, int partId) => throw new NotImplementedException();
        #endregion CRUD
        private static ErrandPart ToErrandPart(ErrandPartDTO item)
        {
            return new ErrandPart(
                item.Id,
                item.ErrandId,
                item.PartId,
                item.Amount,
                item.OrderedById
            );
        }
    }
}