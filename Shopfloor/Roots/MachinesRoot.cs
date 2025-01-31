using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Models.MachinesResponsibles;
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
        private readonly IRepository<MachineResponsible, MachineResponsibleCreation> _responsiblesData;
        public MachinesRoot(
            IRepository<Machine, MachineCreation> machineData,
            IRepository<Person, PersonCreation> personData,
            IRepository<Line, LineCreation> lineData,
            IRepository<Workshop, WorkshopCreation> workshopsData,
            IRepository<MachineResponsible, MachineResponsibleCreation> responsiblesData)
        {
            _machinesData = machineData;
            _personData = personData;
            _linesData = lineData;
            _workshopsData = workshopsData;
            _responsiblesData = responsiblesData;
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
        public async Task UpdateResponsibles(Machine machine, List<Person> persons)
        {
            int machineId = machine.Id;
            List<Person> peopleToDelete = [];
            List<Person> peopleToAdd = [];
            List<Person> peopleExisting = machine.Responsibles;

            peopleToDelete.AddRange(peopleExisting.Except(persons));
            peopleToAdd.AddRange(persons.Except(peopleExisting));

            List<Task> tasks = [];

            tasks.AddRange(ResponsiblesTaskCreate(machineId, peopleToAdd));
            tasks.AddRange(ResponsiblesTaskDelete(peopleToDelete));

            await Task.WhenAll(tasks);

            machine.Responsibles.Clear();
            machine.ResponsibleIds.Clear();

            machine.Responsibles.AddRange(persons);
            machine.ResponsibleIds.AddRange(persons.Select(person => person.Id));
        }
        protected void OnDataChanged(EventArgs e) => DataChanged?.Invoke(this, e);
        private List<Task> ResponsiblesTaskCreate(int machineId, List<Person> people)
        {
            List<Task> tasks = [];

            foreach (Person person in people)
            {
                tasks.Add(_responsiblesData.Create(new MachineResponsibleCreation() { MachineId = machineId, PersonId = person.Id }));
            }

            return tasks;
        }
        private async Task<List<Task>> ResponsiblesTaskDelete(List<Person> people)
        {
            List<Task> tasks = [];
            List<MachineResponsible> responsibled = await _responsiblesData.GetDataAsync();

            foreach (Person person in people)
            {
                int? id = responsibled.FirstOrDefault(x => x.Id == person.Id)?.Id;
                if (id != null)
                {
                    tasks.Add(_responsiblesData.Delete((int)id));
                }
            }

            return tasks;
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
        private async Task DecorateWithPersons(IEnumerable<Machine> machines)
        {
            IEnumerable<MachineResponsible> responsibles = await _responsiblesData.GetDataAsync();

            IEnumerable<Person> data = await _personData.GetDataAsync();
            if (!_personData.Merges.Contains(typeof(Workshop)))
            {
                _ = Task.Run(() => DecoratePersonsWithWorkshops(data));
            }

            foreach (Machine machine in machines)
            {
                List<MachineResponsible> persons = responsibles.Where(x => x.MachineId == machine.Id).ToList();
                machine.ResponsibleIds.AddRange(persons
                    .Select(x => x.PersonId));
                machine.Responsibles.AddRange(data
                    .Where(x => machine.ResponsibleIds.
                        Contains(x.Id)));
            }

            _machinesData.Merges.Add(typeof(Person));
        }
    }
}