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
    public class PermissionProvider : IProvider<Permission>
    {
        private readonly DatabaseConnectionFactory _database;

        #region SQLCommands
        private const string _createSQL = @"
            INSERT INTO permissions (part_type_name)
            VALUES (@Value)
            ";
        private const string _getOneSQL = @"
            SELECT *
            FROM permissions
            WHERE id = @Id
            ";
        private const string _getAllSQL = @"
            SELECT *
            FROM permissions
            ";
        private const string _updateSQL = @"
            UPDATE permissions
            SET part_type_name = @Value
            WHERE id = @Id
            ";
        private const string _deleteSQL = @"
            DELETE
            FROM permissions
            WHERE id = @Id
            ";
        #endregion

        public PermissionProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        #region CRUD
        public async Task Create(Permission item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Value = item.Value
            };
            await connection.ExecuteAsync(_createSQL, parameters);
        }
        public async Task<IEnumerable<Permission>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<PermissionDTO> partTypeDTOs = await connection.QueryAsync<PermissionDTO>(_getAllSQL);
            return partTypeDTOs.Select(ToPermission);
        }
        public async Task<Permission> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            PermissionDTO? partTypeDTO = await connection.QuerySingleAsync<PermissionDTO>(_getOneSQL, parameters);
            return ToPermission(partTypeDTO);

        }
        public async Task Update(Permission item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                Value = item.Value
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

        private static Permission ToPermission(PermissionDTO item)
        {
            return new Permission(item.Id, item.Value);
        }
    }
}