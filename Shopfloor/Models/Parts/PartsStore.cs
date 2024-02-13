using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;


using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.PartModel
{
    internal sealed class PartsStore : IDataStore<Part>
    {
        private IEnumerable<Part> _data = [];
        private readonly IServiceProvider _databaseServices;

        public IEnumerable<Part> Data => _data;
        public bool IsLoaded { get; private set; }

        public PartsStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }

        public Task Load()
        {
            PartProvider provider = _databaseServices.GetRequiredService<PartProvider>();
            _data = provider.GetAll().Result;
            IsLoaded = true;
            return Task.CompletedTask;
        }
    }
}