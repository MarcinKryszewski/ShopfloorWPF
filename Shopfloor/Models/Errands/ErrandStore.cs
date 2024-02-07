using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

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
            try
            {
                _data = provider.GetAll().Result;
                IsLoaded = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            ErrandProvider provider = _databaseServices.GetRequiredService<ErrandProvider>();
            _data = await provider.GetAll();
        }
    }
}