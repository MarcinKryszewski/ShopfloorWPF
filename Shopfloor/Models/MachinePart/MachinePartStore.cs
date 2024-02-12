using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Shopfloor.Models.MachinePartModel
{
    public class MachinePartStore : IDataStore<MachinePart>
    {
        private readonly IServiceProvider _databaseServices;
        private IEnumerable<MachinePart> _data = [];
        public MachinePartStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public IEnumerable<MachinePart> Data => _data;
        public bool IsLoaded { get; private set; }
        public Task Load()
        {
            MachinePartProvider provider = _databaseServices.GetRequiredService<MachinePartProvider>();
            _data = provider.GetAll().Result;
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            MachinePartProvider provider = _databaseServices.GetRequiredService<MachinePartProvider>();
            _data = await provider.GetAll();
        }
    }
}