using Dapper;
using Shopfloor.Database;
using Shopfloor.Database.DTOs;
using Shopfloor.Interfaces;
using Shopfloor.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Services.Providers
{
    public class RoleProvider : IProvider<Role>
    {
        private readonly DatabaseConnectionFactory _database;

        #region SQLCommands

        private const string _getOneSQL = @"
            SELECT *
            FROM roles
            WHERE id = @Id
            ";

        private const string _getAllSQL = @"
            SELECT *
            FROM roles
            ";

        #endregion SQLCommands

        public RoleProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        #region CRUD

        public Task<int> Create(Role item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<RoleDTO> roleDTOs = await connection.QueryAsync<RoleDTO>(_getAllSQL);
            return roleDTOs.Select(ToRole);
        }

        public async Task<Role> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            RoleDTO? roleDTO = await connection.QuerySingleAsync<RoleDTO>(_getOneSQL, parameters);
            return ToRole(roleDTO);
        }

        public Task Update(Role item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        #endregion CRUD

        private static Role ToRole(RoleDTO item)
        {
            return new Role(item.Id, item.Role_Name, item.Role_Value);
        }
    }
}