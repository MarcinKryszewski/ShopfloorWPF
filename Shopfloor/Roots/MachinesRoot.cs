using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Persons;
using Shopfloor.Utilities.Collections;

namespace Shopfloor.Roots
{
    internal class MachinesRoot : IRoot
    {
        private readonly IRepository<Machine, MachineCreation> _machinesData;
        private readonly IRepository<Person, PersonCreation> _personData;
        private readonly IRepository<Line, LineCreation> _linesData;
        public MachinesRoot(
            IRepository<Machine, MachineCreation> machineData,
            IRepository<Person, PersonCreation> personData,
            IRepository<Line, LineCreation> lineData)
        {
            _machinesData = machineData;
            _personData = personData;
            _linesData = lineData;
        }
        public event EventHandler? DataChanged;
        public ConcurrentObservableCollection<Machine> Data { get; private set; } = [];
        public async Task GetData()
        {
            List<Machine> data = await _machinesData.GetDataAsync();
            List<Task> merges = [];

            if (!_machinesData.Merges.Contains(typeof(Person)))
            {
                merges.Add(DecorateWithPersons(data));
            }

            if (!_machinesData.Merges.Contains(typeof(Line)))
            {
                merges.Add(DecorateWithPersons(data));
            }

            await Task.WhenAll(merges);

            await Task.Run(() => Parallel.ForEach(data, item =>
            {
                Data.Add(item);
            }));

            OnDataChanged(EventArgs.Empty);
        }
        public async Task CreateMachine(MachineCreation data)
        {

        }
        public async Task UpdateMachine(MachineCreation data)
        {

        }
        public async Task DeleteMachine(MachineCreation data)
        {

        }
        protected void OnDataChanged(EventArgs e) => DataChanged?.Invoke(this, e);
        private async Task DecorateWithPersons(IEnumerable<Machine> machines)
        {

        }
        private async Task DecorateWithLines(IEnumerable<Machine> machines)
        {
            IEnumerable<Line> lines = await _linesData.GetDataAsync();

            foreach (Machine machine in machines)
            {
                machine.Line = lines.FirstOrDefault(x => machine.LineId == x.Id);
            }

            _machinesData.Merges.Add(typeof(Line));

        }
    }
}