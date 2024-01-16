using Dapper;
using Shopfloor.Database;
using Shopfloor.Database.DTOs;
using Shopfloor.Interfaces;
using Shopfloor.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Services.Providers
{
    public class SupplierProvider : IProvider<Supplier>
    {
        private readonly DatabaseConnectionFactory _database;

        #region SQLCommands
        private const string _createSQL = @"
            INSERT INTO suppliers (name)
            VALUES (@Name)
            ";
        private const string _getOneSQL = @"
            SELECT *
            FROM suppliers
            WHERE id = @Id
            ";
        private const string _getAllSQL = @"
            SELECT *
            FROM suppliers
            ";
        private const string _getAllActiveSQL = @"
            SELECT *
            FROM suppliers
            WHERE active = TRUE
            ";
        private const string _updateSQL = @"
            UPDATE suppliers
            SET 
                name = @Name,
                actove = @Active
            WHERE id = @Id
            ";
        private const string _deleteSQL = @"
            DELETE
            FROM suppliers
            WHERE id = @Id
            ";
        #endregion

        public SupplierProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        #region CRUD
        public async Task<int> Create(Supplier item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Name = item.Name
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            return 0;
        }
        public async Task<IEnumerable<Supplier>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<SupplierDTO> supplierDTOs = await connection.QueryAsync<SupplierDTO>(_getAllSQL);
            return supplierDTOs.Select(ToSupplier);
        }
        public async Task<IEnumerable<Supplier>> GetAllActive()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<SupplierDTO> supplierDTOs = await connection.QueryAsync<SupplierDTO>(_getAllActiveSQL);
            return supplierDTOs.Select(ToSupplier);
        }
        public async Task<Supplier> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            SupplierDTO? supplierDTO = await connection.QuerySingleAsync<SupplierDTO>(_getOneSQL, parameters);
            return ToSupplier(supplierDTO);
        }
        public async Task Update(Supplier item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                Name = item.Name,
                Active = item.IsActive
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
        #endregion

        private static Supplier ToSupplier(SupplierDTO item)
        {
            return new Supplier(item.Id, item.Name, item.Active);
        }
    }
}