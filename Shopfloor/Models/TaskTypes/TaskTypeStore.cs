using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.TaskTypeModel
{
    public class TaskTypeStore : IDataStore<TaskType>
    {
        private readonly IServiceProvider _databaseServices;
        private IEnumerable<TaskType> _data = [];
        public TaskTypeStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public IEnumerable<TaskType> Data => _data;
        public bool IsLoaded { get; private set; } = false;
        public Task Load()
        {
            TaskTypeProvider provider = _databaseServices.GetRequiredService<TaskTypeProvider>();
            _data = provider.GetAll().Result;
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            TaskTypeProvider provider = _databaseServices.GetRequiredService<TaskTypeProvider>();
            _data = await provider.GetAll();
        }
    }
}