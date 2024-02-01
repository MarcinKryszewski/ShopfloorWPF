using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandStatusModel
{
    internal sealed class ErrandStatusProvider : IProvider<ErrandStatus>
    {
        private readonly DatabaseConnectionFactory _database;
        private const string _getOneSQL = @"
            SELECT *
            FROM errand_statuses
            WHERE id = @Id
            ";
        private const string _getAllSQL = @"
            SELECT *
            FROM errand_statuses
            ";
        public ErrandStatusProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        public async Task<IEnumerable<ErrandStatus>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<ErrandStatusDTO> errandStatusDTOs = await connection.QueryAsync<ErrandStatusDTO>(_getAllSQL);
            return errandStatusDTOs.Select(ToErrandStatus);
        }
        public async Task<ErrandStatus> GetById(int id)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Id = id
            };
            ErrandStatusDTO? errandStatusDTO = await connection.QuerySingleAsync<ErrandStatusDTO>(_getOneSQL, parameters);
            return ToErrandStatus(errandStatusDTO);
        }
        #region  NOT IMPLEMENTED
        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }
        Task<int> IProvider<ErrandStatus>.Create(ErrandStatus item)
        {
            throw new System.NotImplementedException();
        }
        Task IProvider<ErrandStatus>.Update(ErrandStatus item)
        {
            throw new System.NotImplementedException();
        }
        #endregion NOT IMPLEMENTED
        private static ErrandStatus ToErrandStatus(ErrandStatusDTO item)
        {
            return new ErrandStatus(item.Id, item.Description);
        }
    }
}