using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.MachinePartModel
{
    internal sealed class MachinePartStore : IDataStore<MachinePart>
    {
        private readonly IServiceProvider _databaseServices;
        private List<MachinePart> _data = [];
        public MachinePartStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public List<MachinePart> Data => _data;
        public bool IsLoaded { get; private set; }

        public Task CombineData()
        {
            throw new NotImplementedException();
        }

        public Task Load()
        {
            MachinePartProvider provider = _databaseServices.GetRequiredService<MachinePartProvider>();
            _data = new(provider.GetAll().Result);
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            MachinePartProvider provider = _databaseServices.GetRequiredService<MachinePartProvider>();
            _data = new(await provider.GetAll());
        }
    }
}