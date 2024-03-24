using Shopfloor.Interfaces;
using Shopfloor.Models.MachineModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandToMachine : ICombiner
    {
        private readonly ErrandStore _errandStore;

        private readonly MachineStore _machineStore;
        public ErrandToMachine(ErrandStore errandStore, MachineStore machineStore)
        {
            _errandStore = errandStore;
            _machineStore = machineStore;
        }
        public Task Combine()
        {
            List<Errand> errands = LoadErrands();
            List<Machine> machines = LoadMachines();
            foreach (Errand errand in errands)
            {
                errand.Machine = machines.FirstOrDefault(machine => machine.Id == errand.MachineId);
            }
            return Task.CompletedTask;
        }
        private List<Machine> LoadMachines() => _machineStore.GetData();
        private List<Errand> LoadErrands() => _errandStore.GetData();
    }
}