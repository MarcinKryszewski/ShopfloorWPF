using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandPartOrderModel
{
    internal sealed class ErrandPartOrderProvider : IProvider<ErrandPartOrder>
    {
        private const string _createSQL = @"
            INSERT INTO errandPartOrders (errand_part, order)
            VALUES (@ErrandPartId, @OrderId)
            ";
        private const string _deleteSQL = @"
            DELETE
            FROM errandPartOrders
            WHERE id = @Id
            ";
        private const string _getAllSQL = @"
            SELECT
                id AS Id,
                errand_part AS ErrandPartId,
                order as OrderId
            FROM errandPartOrders
            ";
        private const string _getOneSQL = @"
            SELECT
                id AS Id,
                errand_part AS ErrandPartId,
                order as OrderId
            FROM errandPartOrders
            WHERE id = @Id
            ";
        private const string _updateSQL = @"
            UPDATE errandPartOrders
            SET
                errand_part AS ErrandPartId,
                order as OrderId
            WHERE id = @Id
            ";
        private readonly DatabaseConnectionFactory _database;
        public ErrandPartOrderProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<int> Create(ErrandPartOrder item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                ErrandPartId = item.ErrandPartId,
                OrderId = item.OrderId,
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
        public async Task<IEnumerable<ErrandPartOrder>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<ErrandPartOrderDto> errandPartOrderDTOs = await connection.QueryAsync<ErrandPartOrderDto>(_getAllSQL);
            return errandPartOrderDTOs.Select(ToModel);
        }
        public async Task<ErrandPartOrder> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
            };
            ErrandPartOrderDto? errandPartOrderDTO = await connection.QuerySingleAsync<ErrandPartOrderDto>(_getOneSQL, parameters);
            return ToModel(errandPartOrderDTO);
        }
        public async Task Update(ErrandPartOrder item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                ErrandPartId = item.ErrandPartId,
                OrderId = item.OrderId,
            };
            await connection.ExecuteAsync(_updateSQL, parameters);
        }
        private static ErrandPartOrder ToModel(ErrandPartOrderDto item)
        {
            return new ErrandPartOrder()
            {
                Id = (int)item.Id!,
                ErrandPartId = item.ErrandPartId,
                OrderId = item.OrderId,
            };
        }
    }
}