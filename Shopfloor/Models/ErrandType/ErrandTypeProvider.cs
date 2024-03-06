using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandTypeModel
{
    internal sealed class ErrandTypeProvider : IProvider<ErrandType>
    {
        private readonly DatabaseConnectionFactory _database;
        public ErrandTypeProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        private const string _getAllSQL = @"
            SELECT *
            FROM errand_types
            ";
        public async Task<IEnumerable<ErrandType>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<ErrandTypeDTO> errandTypeDTOs = await connection.QueryAsync<ErrandTypeDTO>(_getAllSQL);
            return errandTypeDTOs.Select(ToErrandType);
        }
        private static ErrandType ToErrandType(ErrandTypeDTO item)
        {
            return new ErrandType((int)item.Id!, item.Name, item.Description);
        }
        #region NOT_IMPLEMENTED
        public Task<int> Create(ErrandType item) => throw new NotImplementedException();
        public Task Delete(int id) => Task.CompletedTask;

        public Task<ErrandType> GetById(int id) => throw new NotImplementedException();
        public Task UpdateAmount(ErrandType item) => Task.CompletedTask;
        #endregion NOT_IMPLEMENTED
    }
}