using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;

namespace Shopfloor.Models.UserModel
{
    internal sealed class UserProvider : IUserProvider
    {
        private const string _createSQL = @"
            INSERT INTO users (username, user_name, user_surname, image_path)
            VALUES (@Username, @Name, @Surname, @ImagePath)
            ";
        private const string _deleteSQL = @"
            DELETE
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
        private const string _getByUsername = @"
            SELECT
                id AS Id,
                username AS Username,
                user_name AS Name,
                user_surname AS Surname,
                image_path AS ImagePath,
                active AS IsActive
            FROM users
            WHERE username = @Username AND active = 1
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
        private const string _setUserActive = @"
            UPDATE users
            SET
                active = @Active
            WHERE id = @Id
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
        private readonly DatabaseConnectionFactory _database;
        public UserProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        public async Task<int> Create(User item)
        {
            User? existingUser = await GetByUsername(item.Username);
            if (existingUser is not null)
            {
                return -1;
            }

            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Username = item.Username,
                Name = item.Name,
                Surname = item.Surname,
                ImagePath = item.Image,
            };
            await connection.ExecuteAsync(_createSQL, parameters);

            string lastIdSQL = _database.DatabaseType switch
            {
                "SQLite" => "SELECT last_insert_rowid()",
                _ => string.Empty,
            };

            return await connection.QueryFirstAsync<int>(lastIdSQL);
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
        public async Task<IEnumerable<User>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<UserDto> userDTOs = await connection.QueryAsync<UserDto>(_getAllSQL);
            return userDTOs.Select(ToUser);
        }
        public async Task<User> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
            };
            UserDto? userDTO = await connection.QuerySingleAsync<UserDto>(_getOneSQL, parameters);
            return ToUser(userDTO);
        }
        public async Task<User?> GetByUsername(string username)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Username = username,
            };
            UserDto? userDTO = await connection.QuerySingleOrDefaultAsync<UserDto>(_getByUsername, parameters);
            if (userDTO == null)
            {
                return null;
            }

            return ToUser(userDTO);
        }
        public async Task SetUserActive(int id, bool isActive)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
                Active = isActive,
            };
            await connection.ExecuteAsync(_setUserActive, parameters);
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
                Active = item.IsActive,
            };
            await connection.ExecuteAsync(_updateSQL, parameters);
        }
        private static User ToUser(UserDto item)
        {
            return new User()
            {
                Id = (int)item.Id!,
                Username = item.Username,
                Name = item.Name,
                Surname = item.Surname,
                Image = item.ImagePath,
                IsActive = item.IsActive,
            };
        }
    }
}