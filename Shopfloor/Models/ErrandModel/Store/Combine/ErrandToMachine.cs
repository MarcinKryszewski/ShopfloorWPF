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
        public Task CombineAll()
        {
            List<Errand> errands = GetErrands();
            List<Machine> machines = LoadMachines();

            foreach (Errand item in errands)
            {
                Combine(item, machines);
            }
            return Task.CompletedTask;
        }
        public Task CombineOne(Errand item)
        {
            List<Machine> machines = LoadMachines();

            Combine(item, machines);

            return Task.CompletedTask;
        }
        private static void Combine(Errand errand, List<Machine> machines)
        {
            errand.Machine = machines.FirstOrDefault(machine => machine.Id == errand.MachineId);
        }
        private List<Errand> GetErrands() => _errandStore.Data;
        private List<Machine> LoadMachines() => _machineStore.Data;
    }
}