using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Stores.DatabaseDataStores
{
    public class MachineStore : IDataStore<Machine>
    {
        private IEnumerable<Machine> _data = Enumerable.Empty<Machine>();
        private readonly IServiceProvider _databaseServices;

        public IEnumerable<Machine> Data => _data;
        public bool IsLoaded { get; private set; }

        public MachineStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }

        public Task Load()
        {
            IProvider<Machine> provider = _databaseServices.GetRequiredService<MachineProvider>();
            _data = provider.GetAll().Result;
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            IProvider<Machine> provider = _databaseServices.GetRequiredService<MachineProvider>();
            _data = await provider.GetAll();
            //IsLoaded = true;
            //return Task.CompletedTask;
        }
    }
}