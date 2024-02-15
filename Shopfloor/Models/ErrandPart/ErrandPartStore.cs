using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel
{
    internal sealed class ErrandPartStore : IDataStore<ErrandPart>
    {
        private readonly IServiceProvider _databaseServices;
        private List<ErrandPart> _data = [];
        public ErrandPartStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public List<ErrandPart> Data => new(_data);
        public bool IsLoaded { get; private set; }
        public Task Load()
        {
            ErrandPartProvider provider = _databaseServices.GetRequiredService<ErrandPartProvider>();
            _data = new(provider.GetAll().Result);
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            ErrandPartProvider provider = _databaseServices.GetRequiredService<ErrandPartProvider>();
            _data = new(await provider.GetAll());
        }
    }
}