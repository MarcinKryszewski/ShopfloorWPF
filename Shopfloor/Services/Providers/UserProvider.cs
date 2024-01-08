using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;
using Shopfloor.Database.DTOs;
using Shopfloor.Interfaces;
using Shopfloor.Models;

namespace Shopfloor.Services.Providers
{
    public class UserProvider : IProvider<User>
    {
        private readonly DatabaseConnectionFactory _database;

        #region SQLCommands
        private const string _createSQL = @"
            INSERT INTO users (username)
            VALUES (@Username)
            ";
        private const string _getOneSQL = @"
            SELECT *
            FROM users
            WHERE id = @Id
            ";
        private const string _getAllSQL = @"
            SELECT *
            FROM users
            ";
        private const string _updateSQL = @"
            UPDATE users
            SET username = @Username
            WHERE id = @Id
            ";
        private const string _deleteSQL = @"
            DELETE
            FROM users
            WHERE id = @Id
            ";
        #endregion

        public UserProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        #region CRUD
        public async Task Create(User item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Username = item.Username
            };
            await connection.ExecuteAsync(_createSQL, parameters);
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<UserDTO> userDTOs = await connection.QueryAsync<UserDTO>(_getAllSQL);
            return userDTOs.Select(ToUser);
        }
        public async Task<User> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            UserDTO? userDTO = await connection.QuerySingleAsync<UserDTO>(_getOneSQL, parameters);
            return ToUser(userDTO);

        }
        public async Task Update(User item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                Username = item.Username
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

        private static User ToUser(UserDTO item)
        {
            return new User(item.Id, item.Username);
        }
    }
}