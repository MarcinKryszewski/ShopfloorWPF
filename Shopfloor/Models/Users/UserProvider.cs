using Dapper;
using Shopfloor.Database;

using Shopfloor.Interfaces;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.UserModel
{
    internal sealed class UserProvider : IProvider<User>
    {
        private readonly DatabaseConnectionFactory _database;

        #region SQLCommands

        private const string _createSQL = @"
            INSERT INTO users (username, user_name, user_surname, image_path)
            VALUES (@Username, @Name, @Surname, @ImagePath)
            ";

        private const string _getOneSQL = @"
            SELECT 
                id AS Id,
                username AS Username,
                user_name AS Name,
                user_surname AS Surname,
                image_path AS ImagePath,
                active AS IsActive
            FROM users
            WHERE id = @Id
            ";

        private const string _getAllSQL = @"
            SELECT 
                id AS Id,
                username AS Username,
                user_name AS Name,
                user_surname AS Surname,
                image_path AS ImagePath,
                active AS IsActive
            FROM users
            ";

        private const string _updateSQL = @"
            UPDATE users
            SET
                username = @Username,
                user_name = @Name,
                user_surname = @Surname,
                image_path = @ImagePath,
                active = @Active
            WHERE id = @Id
            ";

        private const string _deleteSQL = @"
            DELETE
            FROM users
            WHERE id = @Id
            ";

        private const string _getByUsername = @"
            SELECT *
            FROM users
            WHERE username = @Username AND active = 1
            ";

        private const string _setUserActive = @"
            UPDATE users
            SET
                active = @Active
            WHERE id = @Id
        ";

        #endregion SQLCommands

        public UserProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        #region CRUD

        public async Task<int> Create(User item)
        {
            User? existingUser = await GetByUsername(item.Username);
            if (existingUser is not null) return -1;

            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Username = item.Username,
                Name = item.Name,
                Surname = item.Surname,
                ImagePath = item.Image
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            string lastIdSQL = _database.DatabaseType switch
            {
                "SQLite" => "SELECT last_insert_rowid()",
                _ => string.Empty,
            };

            return connection.Query<int>(lastIdSQL).Single();
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

        public async Task<User?> GetByUsername(string username)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Username = username
            };
            UserDTO? userDTO = await connection.QuerySingleOrDefaultAsync<UserDTO>(_getByUsername, parameters);
            if (userDTO == null) return null;
            return ToUser(userDTO);
        }

        public async Task Update(User item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                Username = item.Username,
                Name = item.Name,
                Surname = item.Surname,
                ImagePath = item.Image,
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

        public async Task SetUserActive(int id, bool isActive)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
                Active = isActive
            };
            await connection.ExecuteAsync(_setUserActive, parameters);
        }

        #endregion CRUD

        private static User ToUser(UserDTO item)
        {
            return new User
            (
                item.Id,
                item.Username,
                item.Name,
                item.Surname,
                item.ImagePath,
                item.IsActive
            );
        }
    }
}