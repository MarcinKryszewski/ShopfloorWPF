using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.MachineModel.Store.Combine
{
    internal sealed class MachineToMachine : ICombiner<Machine>
    {
        private readonly IDataStore<Machine> _machineStore;
        public MachineToMachine(IDataStore<Machine> machineStore)
        {
            _machineStore = machineStore;
        }
        public Task CombineAll()
        {
            List<Machine> machines = GetMachines();

            foreach (Machine item in machines)
            {
                if (item.ParentId is not null)
                {
                    Combine(machines, item);
                }
            }
            return Task.CompletedTask;
        }
        private static void Combine(List<Machine> machines, Machine item)
        {
            Machine? parent = machines.FirstOrDefault(m => m.Id == item.ParentId);
            if (parent is not null) item.SetParent(parent);
        }
        public Task CombineOne(Machine item)
        {
            List<Machine> machines = GetMachines();

            Combine(machines, item);

            return Task.CompletedTask;
        }
        private List<Machine> GetMachines() => _machineStore.Data;
    }
}