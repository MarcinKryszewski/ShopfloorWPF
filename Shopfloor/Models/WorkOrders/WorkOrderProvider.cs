using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Shopfloor.Database;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.WorkOrders
{
    internal class WorkOrderProvider : IProvider<WorkOrderModel>
    {
        private readonly DatabaseConnectionFactory _database;
        public WorkOrderProvider(DatabaseConnectionFactory databaseConnection)
        {
            _database = databaseConnection;
        }
        public async Task<int> Create(WorkOrderModel item)
        {
            using IDbConnection connection = _database.Connect();
            await Task.Delay(0);
            return 1;
        }
        public async Task Delete(int id)
        {
            using IDbConnection connection = _database.Connect();
            await Task.Delay(0);
        }
        public async Task<IEnumerable<WorkOrderModel>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            await Task.Delay(0);
            return [];
        }
        public async Task<WorkOrderModel> GetById(int id)
        {
            string errorMessage = "Nie znaleziono w bazie danych";
            using IDbConnection connection = _database.Connect();
            await Task.Delay(0);
            return await Task.FromException<WorkOrderModel>(new InvalidOperationException(errorMessage));
        }
        public async Task Update(WorkOrderModel item)
        {
            using IDbConnection connection = _database.Connect();
            await Task.Delay(0);
        }
    }
}