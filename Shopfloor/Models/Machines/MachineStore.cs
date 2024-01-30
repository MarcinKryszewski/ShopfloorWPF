using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.MachineModel
{
    internal sealed class MachineStore : IDataStore<Machine>
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
            List<Machine> machines = new(provider.GetAll().Result);
            foreach (Machine machine in machines)
            {
                if (machine.ParentId is not null)
                {
                    Machine? parent = machines.FirstOrDefault(m => m.Id == machine.ParentId);
                    if (parent is not null) machine.SetParent(parent);
                }
            }
            _data = machines;
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