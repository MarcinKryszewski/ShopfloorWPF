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
    public class PartTypesStore : IDataStore<PartType>
    {
        private IEnumerable<PartType> _data = Enumerable.Empty<PartType>();
        private readonly IServiceProvider _databaseServices;

        public IEnumerable<PartType> Data => _data;
        public bool IsLoaded { get; private set; }

        public PartTypesStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }

        public Task Load()
        {
            IProvider<PartType> provider = _databaseServices.GetRequiredService<PartTypeProvider>();
            _data = provider.GetAll().Result;
            IsLoaded = true;
            return Task.CompletedTask;
        }
    }
}