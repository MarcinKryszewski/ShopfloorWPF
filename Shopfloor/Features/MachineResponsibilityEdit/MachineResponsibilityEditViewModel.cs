using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.MachineResponsibilities;
using Shopfloor.Features.MachineResponsibilityEdit.Commands;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Workshops;
using Shopfloor.Roots;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.MachineResponsibilityEdit
{
    internal class MachineResponsibilityEditViewModel : ViewModelBase
    {
        private readonly MachineContext _context;
        private readonly DataRoot _data;
        private readonly MachinesRoot _machinesRoot;
        private Person? _selectedPerson;
        private ListCollectionView _persons = new(new List<Person>());
        public MachineResponsibilityEditViewModel(
            MachinesRoot machinesRoot,
            MachineContext context,
            ViewModelBaseDependecies dependecies,
            DataRoot data)
        : base(dependecies)
        {
            _context = context;
            _machinesRoot = machinesRoot;
            _data = data;
            ReturnCommand = new NavigationCommand<MachineResponsibilitiesViewModel>(NavigationService).Navigate();
            AddPersonCommand = new(SelectedMachine.Responsibles);
            RemovePersonCommand = new(SelectedMachine.Responsibles);

            AddPersonCommand.DataChanged += OnDataChanged;
            RemovePersonCommand.DataChanged += OnDataChanged;

            Task.Run(LoadDataAsync);
        }
        private bool FilterExistingPeople(object obj)
        {
            if (obj is Person person)
            {
                if (SelectedMachine is null)
                {
                    return true;
                }
                bool personExists = SelectedMachine.Responsibles.Contains(person);

                return !personExists;
            }
            return true;
        }
        public void OnDataChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(MissingWorkshops));
            Application.Current.Dispatcher.Invoke(() =>
            {
                SelectedPersons.Refresh();
                Persons.Refresh();
            });
        }
        public string MissingWorkshops
        {
            get
            {
                if (SelectedMachine is null)
                {
                    return string.Empty;
                }

                IEnumerable<Workshop> workshopsData = _data.GetWorkshop().Result;
                IEnumerable<Workshop> workshops = MissingResponsibles(SelectedMachine, workshopsData);

                if (!workshops.Any())
                {
                    return string.Empty;
                }

                StringBuilder sb = new();
                sb.AppendLine("Warsztaty z brakującą osobą odpowiedzialną:");
                foreach (Workshop workshop in workshops)
                {
                    sb.AppendLine(workshop.Name);
                }
                return sb.ToString();
            }
        }
        public ICollectionView Persons => _persons;
        public ICollectionView SelectedPersons => CollectionViewSource.GetDefaultView(SelectedMachine.Responsibles);
        public ICommand ReturnCommand { get; }
        public AddPersonToListCommand AddPersonCommand { get; }
        public RemovePersonToListCommand RemovePersonCommand { get; }
        public Machine SelectedMachine => _context.Machine!;
        public Person? SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
            }
        }
        public string Title
        {
            get
            {
                if (SelectedMachine is null)
                {
                    return string.Empty;
                }
                string line = SelectedMachine.Line?.Name ?? string.Empty;
                string name = SelectedMachine.Name ?? string.Empty;
                return $"{line} - {name}";
            }
        }
        private static LinkedList<Workshop> MissingResponsibles(Machine machine, IEnumerable<Workshop> workshops)
        {
            IEnumerable<Person> responsibles = machine.Responsibles;
            LinkedList<Workshop> missingWorkshops = [];

            foreach (Workshop workshop in workshops)
            {
                if (responsibles.FirstOrDefault(p => p.Workshop == workshop) is null)
                {
                    missingWorkshops.AddLast(workshop);
                }
            }
            return missingWorkshops;
        }
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            tasks.Add(LoadPersonsAsync(_data));

            await Task.WhenAll(tasks);
        }
        private async Task LoadPersonsAsync(DataRoot dataRoot)
        {
            List<Person> data = (await dataRoot.GetPerson()).ToList();
            _persons = new ListCollectionView(data)
            {
                Filter = FilterExistingPeople,
            };
            OnPropertyChanged(nameof(Persons));
        }
    }
}