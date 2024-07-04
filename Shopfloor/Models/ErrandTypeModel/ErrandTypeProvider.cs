using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandTypeModel
{
    internal sealed class ErrandTypeProvider : IProvider<ErrandType>
    {
        private const string _getAllSQL = @"
            SELECT *
            FROM errand_types
            ";
        private readonly DatabaseConnectionFactory _database;
        public ErrandTypeProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        public Task<int> Create(ErrandType item) => throw new NotImplementedException();
        public Task Delete(int id) => Task.CompletedTask;
        public async Task<IEnumerable<ErrandType>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<ErrandTypeDto> errandTypeDTOs = await connection.QueryAsync<ErrandTypeDto>(_getAllSQL);
            return errandTypeDTOs.Select(ToErrandType);
        }
        public Task<ErrandType> GetById(int id) => throw new NotImplementedException();
        public Task Update(ErrandType item) => Task.CompletedTask;
        private static ErrandType ToErrandType(ErrandTypeDto item)
        {
            return new ErrandType()
            {
                Id = (int)item.Id!,
                Name = item.Name,
                Description = item.Description,
            };
        }
    }
}