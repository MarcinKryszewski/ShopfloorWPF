using Shopfloor.Interfaces;
using Shopfloor.Models.MachineModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandToMachine : ICombiner<Errand>
    {
        private readonly IDataStore<Errand> _errandStore;
        private readonly MachineStore _machineStore;
        public ErrandToMachine(MachineStore machineStore, IDataStore<Errand> errandStore)
        {
            _machineStore = machineStore;
            _errandStore = errandStore;
        }
        public Task Combine()
        {
            List<Errand> errands = GetErrands();
            List<Machine> machines = LoadMachines();

            foreach (Errand errand in errands)
            {
                errand.Machine = machines.FirstOrDefault(machine => machine.Id == errand.MachineId);
            }
            return Task.CompletedTask;
        }
        private List<Errand> GetErrands() => _errandStore.Data;
        private List<Machine> LoadMachines() => _machineStore.Data;
    }
}