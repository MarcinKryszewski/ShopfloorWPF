using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.RoleModel
{
    internal class RoleProvider : IProvider<Role>
    {
        private const string _getAllSQL = @"
            SELECT *
            FROM roles
            ";
        private const string _getOneSQL = @"
            SELECT *
            FROM roles
            WHERE id = @Id
            ";
        private readonly DatabaseConnectionFactory _database;
        public RoleProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        public Task<int> Create(Role item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Role>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<RoleDto> roleDTOs = await connection.QueryAsync<RoleDto>(_getAllSQL);
            return roleDTOs.Select(ToRole);
        }

        public async Task<Role> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
            };
            RoleDto? roleDTO = await connection.QuerySingleAsync<RoleDto>(_getOneSQL, parameters);
            return ToRole(roleDTO);
        }

        public Task Update(Role item)
        {
            throw new NotImplementedException();
        }
        private static Role ToRole(RoleDto item)
        {
            return new Role(item.Id, item.Role_Name, item.Role_Value);
        }
    }
}