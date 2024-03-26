using Shopfloor.Interfaces;
using Shopfloor.Models.MachineModel.Store.Combine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.MachineModel.Store
{
    internal sealed class MachineCombiner : ICombinerManager<Machine>
    {
        private readonly MachineToMachine _machineToMachine;
        private readonly List<Machine> _data;

        public MachineCombiner(MachineStore store, MachineToMachine machineToMachine)
        {
            _machineToMachine = machineToMachine;
            _data = store.Data;
        }
        public async Task Combine()
        {
            List<Task> tasks = [];

            tasks.Add(_machineToMachine.Combine());

            await Task.WhenAll(tasks);
        }
    }
}