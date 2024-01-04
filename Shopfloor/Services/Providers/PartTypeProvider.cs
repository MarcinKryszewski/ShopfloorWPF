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
    public class PartTypeProvider : IProvider<PartType>
    {
        private readonly DatabaseConnectionFactory _database;

        #region SQLCommands
        private const string _createSQL = @"
            INSERT INTO
            parts_types (Name)
            VALUES (@Name)
            ";
        private const string _getOneSQL = @"
            SELECT *
            FROM parts_types
            WHERE Id = @Id
            ";
        private const string _getAllSQL = @"
            SELECT *
            FROM parts_types
            ";
        private const string _updateSQL = @"
            UPDATE parts_types
            SET 
                Name = @Name
            WHERE Id = @Id
            ";
        private const string _deleteSQL = @"
            DELETE
            FROM parts_types
            WHERE Id = @Id
            ";
        #endregion

        public PartTypeProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        #region CRUD
        public async Task Create(PartType item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                name = item.Name
            };
            await connection.ExecuteAsync(_createSQL, parameters);
        }
        public async Task<IEnumerable<PartType>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<PartTypeDTO> partTypeDTOs = await connection.QueryAsync<PartTypeDTO>(_getAllSQL);
            return partTypeDTOs.Select(ToPartType);
        }
        public async Task<PartType> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            PartTypeDTO? partTypeDTO = await connection.QuerySingleAsync<PartTypeDTO>(_getOneSQL, parameters);
            return ToPartType(partTypeDTO);

        }
        public async Task Update(PartType item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                id = item.Id,
                name = item.Name
            };
            await connection.ExecuteAsync(_updateSQL, parameters);
        }
        public async Task Delete(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                id = id
            };
            await connection.ExecuteAsync(_deleteSQL, parameters);
        }
        #endregion

        private static PartType ToPartType(PartTypeDTO item)
        {
            return new PartType(item.Id, item.Name);
        }
    }
}