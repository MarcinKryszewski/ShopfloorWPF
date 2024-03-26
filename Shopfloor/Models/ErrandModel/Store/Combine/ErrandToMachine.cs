using Shopfloor.Interfaces;
using Shopfloor.Models.MachineModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandToMachine : ICombiner<Errand>
    {
        private readonly MachineStore _machineStore;
        public ErrandToMachine(MachineStore machineStore)
        {
            _machineStore = machineStore;
        }
        public Task Combine(List<Errand> data)
        {
            List<Machine> machines = LoadMachines();
            foreach (Errand errand in data)
            {
                errand.Machine = machines.FirstOrDefault(machine => machine.Id == errand.MachineId);
            }
            return Task.CompletedTask;
        }
        private List<Machine> LoadMachines() => _machineStore.GetData();
    }
}