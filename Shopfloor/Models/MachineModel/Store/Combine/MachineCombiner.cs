using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Interfaces;
using Shopfloor.Models.MachineModel.Store.Combine;

namespace Shopfloor.Models.MachineModel.Store
{
    internal sealed class MachineCombiner : ICombiner
    {
        private readonly MachineToMachine _machineToMachine;

        public MachineCombiner(MachineToMachine machineToMachine)
        {
            _machineToMachine = machineToMachine;
        }
        public async Task Combine()
        {
            List<Task> tasks = [];

            tasks.Add(_machineToMachine.Combine());

            await Task.WhenAll(tasks);
        }
    }
}