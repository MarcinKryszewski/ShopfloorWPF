using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;


using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.MachineModel
{
    public class MachineStore : IDataStore<Machine>
    {
        private readonly IServiceProvider _databaseServices;
        private IEnumerable<Machine> _data = [];
        public MachineStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public IEnumerable<Machine> Data => _data;
        public bool IsLoaded { get; private set; }
        public Task Load()
        {
            MachineProvider provider = _databaseServices.GetRequiredService<MachineProvider>();
            _data = provider.GetAll().Result;
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            MachineProvider provider = _databaseServices.GetRequiredService<MachineProvider>();
            _data = await provider.GetAll();
        }
    }
}