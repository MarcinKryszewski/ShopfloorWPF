using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Interfaces;
using Shopfloor.Models.MachineModel.Store.Combine;

namespace Shopfloor.Models.MachineModel.Store
{
    internal sealed class MachineCombiner : ICombinerManager<Machine>
    {
        private readonly List<Machine> _data;
        private readonly MachineToMachine _machineToMachine;
        public MachineCombiner(IDataStore<Machine> store, MachineToMachine machineToMachine)
        {
            _machineToMachine = machineToMachine;
            _data = store.Data;
        }
        public bool IsCombined { get; private set; }
        public async Task CombineAll(bool shouldForce = false)
        {
            if (IsCombined && !shouldForce)
            {
                return;
            }

            List<Task> tasks = [];

            tasks.Add(_machineToMachine.CombineAll());

            await Task.WhenAll(tasks);
            IsCombined = true;
        }

        public async Task CombineOne(Machine item)
        {
            List<Task> tasks = [];

            tasks.Add(_machineToMachine.CombineOne(item));

            await Task.WhenAll(tasks);
        }
    }
}