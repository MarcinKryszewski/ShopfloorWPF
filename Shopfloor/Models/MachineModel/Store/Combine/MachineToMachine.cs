using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.MachineModel.Store.Combine
{
    internal sealed class MachineToMachine : ICombiner<Machine>
    {
        private readonly MachineStore _machineStore;
        public MachineToMachine(MachineStore machineStore)
        {
            _machineStore = machineStore;
        }
        public Task CombineAll()
        {
            List<Machine> machines = GetMachines();

            foreach (Machine machine in machines)
            {
                if (machine.ParentId is not null)
                {
                    Machine? parent = machines.FirstOrDefault(m => m.Id == machine.ParentId);
                    if (parent is not null) machine.SetParent(parent);
                }
            }
            return Task.CompletedTask;
        }
        private List<Machine> GetMachines() => _machineStore.Data;
    }
}