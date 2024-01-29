using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandModel
{
    internal sealed class ErrandStore : IDataStore<Errand>
    {
        private readonly IServiceProvider _databaseServices;
        private IEnumerable<Errand> _data = [];
        public ErrandStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public IEnumerable<Errand> Data => _data;
        public bool IsLoaded { get; private set; }
        public Task Load()
        {
            ErrandProvider provider = _databaseServices.GetRequiredService<ErrandProvider>();
            _data = provider.GetAll().Result;
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            ErrandProvider provider = _databaseServices.GetRequiredService<ErrandProvider>();
            _data = await provider.GetAll();
        }
    }
}