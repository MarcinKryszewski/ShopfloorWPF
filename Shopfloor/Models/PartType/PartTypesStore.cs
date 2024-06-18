using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.PartTypeModel
{
    internal sealed class PartTypesStore : IDataStore<PartType>
    {
        private List<PartType> _data = [];
        private readonly IServiceProvider _databaseServices;

        public List<PartType> Data => _data;
        public bool IsLoaded { get; private set; }

        public PartTypesStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }

        public Task Load()
        {
            IProvider<PartType> provider = _databaseServices.GetRequiredService<PartTypeProvider>();
            _data = new(provider.GetAll().Result);
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            PartTypeProvider provider = _databaseServices.GetRequiredService<PartTypeProvider>();
            _data = new(await provider.GetAll());
        }

        public Task CombineData()
        {
            throw new NotImplementedException();
        }
    }
}