using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandPartModel
{
    public class ErrandPartStore : IDataStore<ErrandPart>
    {
        private readonly IServiceProvider _databaseServices;
        private IEnumerable<ErrandPart> _data = [];
        public ErrandPartStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public IEnumerable<ErrandPart> Data => _data;
        public bool IsLoaded { get; private set; }
        public Task Load()
        {
            ErrandPartProvider provider = _databaseServices.GetRequiredService<ErrandPartProvider>();
            _data = provider.GetAll().Result;
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            ErrandPartProvider provider = _databaseServices.GetRequiredService<ErrandPartProvider>();
            _data = await provider.GetAll();
        }
    }
}