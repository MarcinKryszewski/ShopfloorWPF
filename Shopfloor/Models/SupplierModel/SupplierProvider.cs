using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.SupplierModel
{
    internal sealed class SupplierProvider : IProvider<Supplier>
    {
        private const string _createSQL = @"
            INSERT INTO suppliers (name)
            VALUES (@Name)
            ";
        private const string _deleteSQL = @"
            DELETE
            FROM suppliers
            WHERE id = @Id
            ";
        private const string _getAllActiveSQL = @"
            SELECT *
            FROM suppliers
            WHERE active = TRUE
            ";
        private const string _getAllSQL = @"
            SELECT *
            FROM suppliers
            ";
        private const string _getOneSQL = @"
            SELECT *
            FROM suppliers
            WHERE id = @Id
            ";
        private const string _updateSQL = @"
            UPDATE suppliers
            SET
                name = @Name,
                active = @Active
            WHERE id = @Id
            ";
        private readonly DatabaseConnectionFactory _database;
        public SupplierProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<int> Create(Supplier item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Name = item.Name,
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
        public async Task<IEnumerable<Supplier>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<SupplierDto> supplierDTOs = await connection.QueryAsync<SupplierDto>(_getAllSQL);
            return supplierDTOs.Select(ToSupplier);
        }

        public async Task<IEnumerable<Supplier>> GetAllActive()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<SupplierDto> supplierDTOs = await connection.QueryAsync<SupplierDto>(_getAllActiveSQL);
            return supplierDTOs.Select(ToSupplier);
        }

        public async Task<Supplier> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
            };
            SupplierDto? supplierDTO = await connection.QuerySingleAsync<SupplierDto>(_getOneSQL, parameters);
            return ToSupplier(supplierDTO);
        }

        public async Task Update(Supplier item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                Name = item.Name,
                Active = item.IsActive,
            };
            await connection.ExecuteAsync(_updateSQL, parameters);
        }
        private static Supplier ToSupplier(SupplierDto item)
        {
            return new Supplier((int)item.Id!, item.Name, item.Active);
        }
    }
}