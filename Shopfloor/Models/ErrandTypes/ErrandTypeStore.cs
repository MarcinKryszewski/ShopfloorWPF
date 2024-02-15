using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandTypeModel
{
    internal class ErrandTypeStore : IDataStore<ErrandType>
    {
        private readonly IServiceProvider _databaseServices;
        private List<ErrandType> _data = [];
        public ErrandTypeStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public List<ErrandType> Data => _data;
        public bool IsLoaded { get; private set; } = false;
        public Task Load()
        {
            ErrandTypeProvider provider = _databaseServices.GetRequiredService<ErrandTypeProvider>();
            _data = new(provider.GetAll().Result);
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            ErrandTypeProvider provider = _databaseServices.GetRequiredService<ErrandTypeProvider>();
            _data = new(await provider.GetAll());
        }
    }
}