using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandPartModel
{
    internal sealed class ErrandPartProvider : IProvider<ErrandPart>
    {
        private const string _cancelPartSQL = @"
            UPDATE errands_parts
            SET
                canceled = @Canceled
            WHERE id = @Id
            ";
        private const string _createSQL = @"
            INSERT INTO errands_parts (
                errand_id,
                part_id,
                amount,
                ordered_by_id,
                price_per_unit,
                expected_delivery_date
            )
            VALUES (
                @ErrandId,
                @PartId,
                @Amount,
                @OrderedBy,
                @Price,
                @ExpectedDeliveryDate
            )";
        private const string _dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private const string _getAllSQL = @"
            SELECT
                id as Id,
                errand_id AS ErrandId,
                part_id AS PartId,
                amount AS Amount,
                ordered_by_id as OrderedById,
                price_per_unit as PricePerUnit,
                expected_delivery_date as ExpectedDeliveryDate,
                canceled as Canceled
            FROM errands_parts
            ";
        private const string _getForErrandSQL = @"
            SELECT
                errand_id AS ErrandId,
                part_id AS PartId,
                amount AS Amount,
                ordered_by_id as OrderedById,
                price_per_unit as PricePerUnit,
                expected_delivery_date as ExpectedDeliveryDate,
                canceled as Canceled
            FROM errands_parts
            WHERE errand_id = @ErrandId
            ";
        private const string _getOneSQL = @"
            SELECT
                errand_id AS ErrandId,
                part_id AS PartId,
                amount AS Amount,
                ordered_by_id as OrderedById,
                price_per_unit as PricePerUnit,
                expected_delivery_date as ExpectedDeliveryDate,
                canceled as Canceled
            FROM errands_parts
            WHERE id = @Id
            ";
        private const string _updateAmountSQL = @"
            UPDATE errands_parts
            SET
                amount = @Amount
            WHERE errand_id = @ErrandId AND part_id = @PartId
            ";
        private const string _updateDeliveryDateSQL = @"
            UPDATE errands_parts
            SET
                expected_delivery_date = @ExpectedDeliveryDate
            WHERE id = @Id
            ";
        private const string _updatePriceSQL = @"
            UPDATE errands_parts
            SET
                price_per_unit = @Price
            WHERE id = @Id
            ";
        private readonly DatabaseConnectionFactory _database;
        public ErrandPartProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        public async Task<int> Create(ErrandPart item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                ErrandId = item.ErrandId,
                PartId = item.PartId,
                Amount = item.Amount,
                OrderedBy = item.OrderedById,
                Price = item.PricePerUnit,
                ExpectedDeliveryDate = item.ExpectedDeliveryDate is null ? null : ((DateTime)item.ExpectedDeliveryDate!).ToString(_dateTimeFormat),
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
                ErrandId = id,
                Canceled = true,
            };
            await connection.ExecuteAsync(_cancelPartSQL, parameters);
        }
        public Task Delete(int errandId, int partId) => throw new NotImplementedException();
        public async Task<IEnumerable<ErrandPart>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<ErrandPartDto> errandPartDTOs = await connection.QueryAsync<ErrandPartDto>(_getAllSQL);
            return errandPartDTOs.Select(ToErrandPart);
        }
        public async Task<IEnumerable<ErrandPart>> GetByErrandId(int errandId)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                ErrandId = errandId,
            };
            IEnumerable<ErrandPartDto> errandPartDTOs = await connection.QueryAsync<ErrandPartDto>(_getForErrandSQL, parameters);
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
            };
            await connection.ExecuteAsync(_updateAmountSQL, parameters);
        }
        public async Task UpdateDeliveryDate(int id, DateTime? expectedDeliveryDate)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
                ExpectedDeliveryDate = expectedDeliveryDate,
            };
            await connection.ExecuteAsync(_updateDeliveryDateSQL, parameters);
        }
        public async Task UpdateDeliveryDate(int id, bool cancel)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                ErrandId = id,
                Canceled = cancel,
            };
            await connection.ExecuteAsync(_cancelPartSQL, parameters);
        }
        public async Task UpdatePrice(int id, double pricePerUnit)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
                Price = pricePerUnit,
            };
            await connection.ExecuteAsync(_updatePriceSQL, parameters);
        }
        private static ErrandPart ToErrandPart(ErrandPartDto item)
        {
            return new ErrandPart()
            {
                Id = item.Id,
                Amount = item.Amount,
                ErrandId = item.ErrandId,
                ExpectedDeliveryDate = item.ExpectedDeliveryDate,
                OrderedById = item.OrderedById,
                PartId = item.PartId,
            };
        }
    }
}