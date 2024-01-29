using Dapper;
using Shopfloor.Database;
using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandErrandStatusesModel
{
    internal sealed class ErrandErrandStatusProvider : IProvider<ErrandErrandStatus>
    {
        private readonly DatabaseConnectionFactory _database;
        public ErrandErrandStatusProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        private const string _createSQL = @"
            INSERT INTO errands_errand_statuses (errand_id, errand_status_id, set_date)
            VALUES (@Errand, @Status, @Date)
        ";
        private const string _deleteSQL = @"
            DELETE
            FROM errands_errand_statuses
            WHERE 
                errand_id = @Errand AND
                errand_status_id = @Status
        ";
        private const string _getAllSQL = @"
            SELECT *
            FROM errands_errand_statuses
            ";
        private const string _getOneSQL = @"
            SELECT *
            FROM errands_errand_statuses
            WHERE 
                errand_id = @Errand AND
                errand_status_id = @Status
        ";
        public async Task<int> Create(ErrandErrandStatus item)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Errand = item.ErrandId,
                Status = item.ErrandStatusId,
                Date = item.CreateDate
            };
            await connection.ExecuteAsync(_createSQL, parameters);
            return 0;
        }
        public async Task Delete(int errandId, int statusId)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Errand = errandId,
                Status = statusId
            };
            await connection.ExecuteAsync(_deleteSQL, parameters);
        }
        public async Task<IEnumerable<ErrandErrandStatus>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<ErrandErrandStatusDTO> machineDTOs = await connection.QueryAsync<ErrandErrandStatusDTO>(_getAllSQL);
            return machineDTOs.Select(ToModel);
        }
        public async Task<ErrandErrandStatus> GetById(int errandId, int statusId)
        {
            using IDbConnection connection = _database.Connect();
            object parameters = new
            {
                Errand = errandId,
                Status = statusId
            };
            ErrandErrandStatusDTO? errandErrandStatusDTO = await connection.QuerySingleAsync<ErrandErrandStatusDTO>(_getOneSQL, parameters);
            return ToModel(errandErrandStatusDTO);
        }
        public Task<ErrandErrandStatus> GetById(int id) => throw new System.NotImplementedException();
        public Task Update(ErrandErrandStatus item) => throw new System.NotImplementedException();
        public Task Delete(int id) => throw new System.NotImplementedException();
        private static ErrandErrandStatus ToModel(ErrandErrandStatusDTO item)
        {
            return new(item.Errand_Id, item.Errand_Status_Id, item.Set_Date);
        }
    }
}