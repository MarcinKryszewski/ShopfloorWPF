using Dapper;
using Shopfloor.Database;

using Shopfloor.Interfaces;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.RoleUserModel
{
    internal sealed class RoleUserProvider : IProvider<RoleUser>
    {
        private readonly DatabaseConnectionFactory _database;

        #region SQLCommands

        private const string _createSQL = @"
            INSERT INTO roles_users (role_id, user_id)
            VALUES (@RoleId, @UserId)
            ";

        private const string _getAllSQL = @"
            SELECT
                id AS Id,
                role_id AS RoleId,
                user_id AS UserId
            FROM roles_users
            ";

        private const string _deleteSQL = @"
            DELETE
            FROM roles_users
            WHERE role_id = @RoleId AND user_id = @UserId
            ";

        private const string _getAllForUser = @"
            SELECT
                id AS Id,
                role_id AS RoleId,
                user_id AS UserId
            FROM roles_users
            WHERE user_id = @UserId
            ";

        private const string _getAllForRole = @"
            SELECT
                id AS Id,
                role_id AS RoleId,
                user_id AS UserId
            FROM roles_users
            WHERE role_id = @RoleId
            ";

        #endregion SQLCommands

        public RoleUserProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        #region CRUD

        public async Task<int> Create(RoleUser item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                RoleId = item.RoleId,
                UserId = item.UserId
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            return 0;
        }

        public async Task<int> Create(int RoleId, int UserId)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                RoleId = RoleId,
                UserId = UserId
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            return 0;
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

        #endregion CRUD

        private static RoleUser ToRoleUser(RoleUserDTO item)
        {
            return new RoleUser()
            {
                RoleId = item.RoleId,
                UserId = item.UserId
            }; ;
        }
    }
}