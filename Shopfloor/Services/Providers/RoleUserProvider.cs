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
    public class RoleUserProvider : IProvider<RoleUser>
    {
        private readonly DatabaseConnectionFactory _database;

        #region SQLCommands
        private const string _createSQL = @"
            INSERT INTO roles_users (role, user)
            VALUES (@RoleId, @UserId)
            ";
        private const string _getAllSQL = @"
            SELECT *
            FROM roles_users
            ";
        private const string _deleteSQL = @"
            DELETE
            FROM roles_users
            WHERE role = @RoleID AND user = @UserId
            ";
        private const string _getAllForUser = @"
            SELECT *
            FROM roles_users
            WHERE user = @UserId
            ";
        private const string _getAllForRole = @"
            SELECT *
            FROM roles_users
            WHERE role = @RoleId
            ";
        #endregion

        public RoleUserProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        #region CRUD        
        public async Task Create(RoleUser item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                RoleId = item.RoleId,
                UserId = item.UserId
            };
            await connection.ExecuteAsync(_createSQL, parameters);
        }
        public async Task Delete(int roleId, int userId)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                RoleId = roleId,
                UserId = userId
            };
            await connection.ExecuteAsync(_deleteSQL, parameters);
        }
        public async Task<IEnumerable<RoleUser>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<RoleUserDTO> roleUserDTOs = await connection.QueryAsync<RoleUserDTO>(_getAllSQL);
            return roleUserDTOs.Select(ToRoleUser);
        }
        public async Task<IEnumerable<RoleUser>> GetAllForUser(int userId)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                UserId = userId
            };
            IEnumerable<RoleUserDTO> roleUserDTOs = await connection.QueryAsync<RoleUserDTO>(_getAllForUser, parameters);
            return roleUserDTOs.Select(ToRoleUser);
        }
        public async Task<IEnumerable<RoleUser>> GetAllForRole(int roleId)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                RoleId = roleId
            };
            IEnumerable<RoleUserDTO> roleUserDTOs = await connection.QueryAsync<RoleUserDTO>(_getAllForRole, parameters);
            return roleUserDTOs.Select(ToRoleUser);
        }

        public Task<RoleUser> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public Task Update(RoleUser item)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        private static RoleUser ToRoleUser(RoleUserDTO item)
        {
            return new RoleUser(item.Role, item.User);
        }


    }
}