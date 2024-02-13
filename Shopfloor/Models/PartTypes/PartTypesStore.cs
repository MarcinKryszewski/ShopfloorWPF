using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.PartTypeModel
{
    internal sealed class PartTypesStore : IDataStore<PartType>
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