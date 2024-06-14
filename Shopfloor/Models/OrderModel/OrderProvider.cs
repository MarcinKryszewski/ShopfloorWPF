using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.OrderModel
{
    internal sealed class OrderProvider : IProvider<Order>
    {
        private readonly DatabaseConnectionFactory _database;
        private const string _dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private const string _createSQL = @"
            INSERT INTO orders (delivery_date, creation_date, delivered)
            VALUES @DeliveryDate, @CreationDate, @Delivered)
            ";
        private const string _getOneSQL = @"
            SELECT 
                id AS Id,
                delivery_date AS DeliveryDate,
                creation_date AS CreationDate,
                delivered AS Delivered
            FROM orders
            WHERE id = @Id
            ";
        private const string _getAllSQL = @"
            SELECT 
                id AS Id,
                delivery_date AS DeliveryDate,
                creation_date AS CreationDate,
                delivered AS Delivered
            FROM orders
            ";
        private const string _updateSQL = @"
            UPDATE orders
            SET 
                delivery_date = @DeliveryDate,
                delivered = @Delivered
            WHERE id = @Id
            ";
        private const string _deleteSQL = @"
            DELETE
            FROM orders
            WHERE id = @Id
            ";
        public OrderProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        public async Task<int> Create(Order item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                CreationDate = item.CreationDate.ToString(_dateTimeFormat),
                Delivered = item.Delivered,
                DeliveryDate = item.DeliveryDate is null ? null : ((DateTime)item.DeliveryDate!).ToString(_dateTimeFormat),
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            return 0;
        }
        public async Task<IEnumerable<Order>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<OrderDTO> orderDTOs = await connection.QueryAsync<OrderDTO>(_getAllSQL);
            return orderDTOs.Select(ToOrder);
        }
        public async Task<Order> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            OrderDTO? orderDTO = await connection.QuerySingleAsync<OrderDTO>(_getOneSQL, parameters);
            return ToOrder(orderDTO);
        }
        public async Task Update(Order item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                Delivered = item.Delivered,
                DeliveryDate = item.DeliveryDate is null ? null : ((DateTime)item.DeliveryDate!).ToString(_dateTimeFormat),
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
        private static Order ToOrder(OrderDTO item)
        {
            return new Order()
            {
                CreationDate = item.CreationDate,
                Delivered = item.Delivered,
                DeliveryDate = item.DeliveryDate,
                Id = item.Id
            };
        }
    }
}