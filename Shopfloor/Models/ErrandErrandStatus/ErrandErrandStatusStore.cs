using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandErrandStatusesModel
{
    internal sealed class ErrandErrandStatusStore : IDataStore<ErrandErrandStatus>
    {
        private readonly IServiceProvider _databaseServices;
        private IEnumerable<ErrandErrandStatus> _data = [];
        public ErrandErrandStatusStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public IEnumerable<ErrandErrandStatus> Data => _data;
        public bool IsLoaded { get; private set; }
        public Task Load()
        {
            ErrandErrandStatusProvider provider = _databaseServices.GetRequiredService<ErrandErrandStatusProvider>();
            _data = provider.GetAll().Result;
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            ErrandErrandStatusProvider provider = _databaseServices.GetRequiredService<ErrandErrandStatusProvider>();
            _data = await provider.GetAll();
        }
    }
}