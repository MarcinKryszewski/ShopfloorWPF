using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandStatusModel
{
    internal sealed class ErrandStatusStore : IDataStore<ErrandStatus>
    {
        private readonly IServiceProvider _databaseServices;
        private List<ErrandStatus> _data = [];
        public ErrandStatusStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public List<ErrandStatus> Data => _data;
        public bool IsLoaded { get; private set; }

        public Task CombineData()
        {
            throw new NotImplementedException();
        }

        public Task Load()
        {
            ErrandStatusProvider provider = _databaseServices.GetRequiredService<ErrandStatusProvider>();
            _data = new(provider.GetAll().Result);
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            ErrandStatusProvider provider = _databaseServices.GetRequiredService<ErrandStatusProvider>();
            _data = new(await provider.GetAll());
        }
    }
}