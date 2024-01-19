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
    public class PartsStore : IDataStore<Part>
    {
        private IEnumerable<Part> _data = Enumerable.Empty<Part>();
        private readonly IServiceProvider _databaseServices;

        public IEnumerable<Part> Data => _data;
        public bool IsLoaded { get; private set; }

        public PartsStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }

        public Task Load()
        {
            IProvider<Part> provider = _databaseServices.GetRequiredService<PartProvider>();
            _data = provider.GetAll().Result;
            IsLoaded = true;
            return Task.CompletedTask;
        }
    }
}