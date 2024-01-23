using Dapper;
using Shopfloor.Database;
using Shopfloor.Database.DTOs;
using Shopfloor.Interfaces;
using Shopfloor.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Services.Providers
{
    public sealed class TaskTypeProvider : IProvider<TaskType>
    {
        private readonly DatabaseConnectionFactory _database;
        public TaskTypeProvider(DatabaseConnectionFactory database)
        {
            _database = database;
        }
        private const string _getAllSQL = @"
            SELECT *
            FROM task_types
            ";
        public async Task<IEnumerable<TaskType>> GetAll()
        {
            using IDbConnection connection = _database.Connect();
            IEnumerable<TaskTypeDTO> taskTypeDTOs = await connection.QueryAsync<TaskTypeDTO>(_getAllSQL);
            return taskTypeDTOs.Select(ToTaskType);
        }
        private static TaskType ToTaskType(TaskTypeDTO item)
        {
            return new TaskType(item.Id, item.Name, item.Description);
        }
        #region NOT_IMPLEMENTED
        public Task<int> Create(TaskType item) => throw new NotImplementedException();
        public Task Delete(int id) => Task.CompletedTask;

        public Task<TaskType> GetById(int id) => throw new NotImplementedException();
        public Task Update(TaskType item) => Task.CompletedTask;
        #endregion NOT_IMPLEMENTED        
    }
}