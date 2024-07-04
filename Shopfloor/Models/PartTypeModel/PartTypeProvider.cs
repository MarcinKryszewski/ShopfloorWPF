using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.PartTypeModel
{
    internal sealed class PartTypeProvider : IProvider<PartType>
    {
        private const string _createSQL = @"
            INSERT INTO parts_types (part_type_name)
            VALUES (@Name)
            ";
        private const string _deleteSQL = @"
            DELETE
            FROM parts_types
            WHERE id = @Id
            ";
        private const string _getAllSQL = @"
            SELECT
                id AS Id,
                part_type_name AS Name
            FROM parts_types
            ";
        private const string _getOneSQL = @"
            SELECT
                id AS Id,
                part_type_name AS Name
            FROM parts_types
            WHERE id = @Id
            ";
        private const string _updateSQL = @"
            UPDATE parts_types
            SET part_type_name = @Name
            WHERE id = @Id
            ";
        private readonly DatabaseConnectionFactory _database;
        public PartTypeProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<int> Create(PartType item)
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
        public async Task<IEnumerable<PartType>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<PartTypeDto> partTypeDTOs = await connection.QueryAsync<PartTypeDto>(_getAllSQL);
            return partTypeDTOs.Select(ToPartType);
        }

        public async Task<PartType> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id,
            };
            PartTypeDto? partTypeDTO = await connection.QuerySingleAsync<PartTypeDto>(_getOneSQL, parameters);
            return ToPartType(partTypeDTO);
        }

        public async Task Update(PartType item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = item.Id,
                Name = item.Name,
            };
            await connection.ExecuteAsync(_updateSQL, parameters);
        }
        private static PartType ToPartType(PartTypeDto item)
        {
            return new PartType((int)item.Id!, item.Name);
        }
    }
}