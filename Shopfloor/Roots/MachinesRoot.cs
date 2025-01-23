using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Workshops;
using Shopfloor.Utilities.Collections;

namespace Shopfloor.Roots
{
    internal class MachinesRoot : IRoot
    {
        private readonly IRepository<Machine, MachineCreation> _machinesData;
        private readonly IRepository<Person, PersonCreation> _personData;
        private readonly IRepository<Line, LineCreation> _linesData;
        private readonly IRepository<Workshop, WorkshopCreation> _workshopsData;
        public MachinesRoot(
            IRepository<Machine, MachineCreation> machineData,
            IRepository<Person, PersonCreation> personData,
            IRepository<Line, LineCreation> lineData,
            IRepository<Workshop, WorkshopCreation> workshopsData)
        {
            _machinesData = machineData;
            _personData = personData;
            _linesData = lineData;
            _workshopsData = workshopsData;
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
                merges.Add(DecorateWithLines(data));
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
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
        public async Task UpdateMachine(MachineCreation data)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
        public async Task DeleteMachine(MachineCreation data)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
        protected void OnDataChanged(EventArgs e) => DataChanged?.Invoke(this, e);
        private async Task DecorateWithPersons(IEnumerable<Machine> machines)
        {
            IEnumerable<Person> data = await _personData.GetDataAsync();
            if (!_personData.Merges.Contains(typeof(Workshop)))
            {
                _ = Task.Run(() => DecoratePersonsWithWorkshops(data));
            }

            foreach (Machine machine in machines)
            {
                if (machine.ResponsibleIds.Count == 0)
                {
                    return;
                }
                foreach (int item in machine.ResponsibleIds)
                {
                    machine.Responsibles.Add(data.First(p => p.Id == item));
                }
            }
            _machinesData.Merges.Add(typeof(Machine));
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
        private async Task DecoratePersonsWithWorkshops(IEnumerable<Person> data)
        {
            IEnumerable<Workshop> workshops = await _workshopsData.GetDataAsync();

            foreach (Person person in data)
            {
                person.Workshop = workshops.FirstOrDefault(x => person.WorkshopId == x.Id);
            }

            _machinesData.Merges.Add(typeof(Line));
        }
    }
}