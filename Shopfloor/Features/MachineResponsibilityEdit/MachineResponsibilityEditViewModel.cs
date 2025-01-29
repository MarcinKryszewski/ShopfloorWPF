using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;
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
        private readonly Machine _selectedMachine;
        private readonly List<Person> _selectedPersons = [];
        private ListCollectionView _persons = new(new List<Person>());
        private Person? _selectedPerson;
        private ActionCommand? cancelCommand;
        public MachineResponsibilityEditViewModel(
            MachineContext context,
            ViewModelBaseDependecies dependecies,
            MachinesRoot machinesRoot,
            DataRoot data)
        : base(dependecies)
        {
            ReturnCommand = new NavigationCommand<MachineResponsibilitiesViewModel>(NavigationService).Navigate();

            _data = data;
            _context = context;

            if (_context.Machine is null)
            {
                ReturnCommand.Execute(null);
            }

            _selectedMachine = _context.Machine!;

            AddPersonCommand = new AddPersonToListCommand(_selectedPersons);
            RemovePersonCommand = new RemovePersonToListCommand(_selectedPersons);
            SaveCommand = new SaveResponsiblesCommand(_selectedMachine, _selectedPersons, machinesRoot);

            ((AddPersonToListCommand)AddPersonCommand).DataChanged += OnDataChanged;
            ((RemovePersonToListCommand)RemovePersonCommand).DataChanged += OnDataChanged;

            Task.Run(LoadDataAsync);
        }
        public ICommand AddPersonCommand { get; }
        public ICommand CancelCommand => cancelCommand ??= new ActionCommand(Cancel);
        public string MissingWorkshops
        {
            get
            {
                IEnumerable<Workshop> workshopsData = _data.GetWorkshop().Result;
                IEnumerable<Workshop> workshops = MissingResponsibles(_selectedPersons, workshopsData);

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
        public ICommand RemovePersonCommand { get; }
        public ICommand ReturnCommand { get; }
        public ICommand SaveCommand { get; }
        public Person? SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
            }
        }
        public ICollectionView SelectedPersons => CollectionViewSource.GetDefaultView(_selectedPersons);
        public string Title
        {
            get
            {
                if (_selectedMachine is null)
                {
                    return string.Empty;
                }
                string line = _selectedMachine.Line?.Name ?? string.Empty;
                string name = _selectedMachine.Name ?? string.Empty;
                return $"{line} - {name}";
            }
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
        private static LinkedList<Workshop> MissingResponsibles(IEnumerable<Person> responsibles, IEnumerable<Workshop> workshops)
        {
            LinkedList<Workshop> missingWorkshops = [];
            foreach (Workshop workshop in from Workshop workshop in workshops
                                          where responsibles.FirstOrDefault(p => p.Workshop == workshop) is null
                                          select workshop)
            {
                missingWorkshops.AddLast(workshop);
            }

            return missingWorkshops;
        }
        private void Cancel()
        {
            LoadSelectedPersonsAsync();
            OnDataChanged(null, new EventArgs());
        }
        private bool FilterExistingPeople(object obj)
        {
            if (obj is Person person)
            {
                if (_selectedMachine is null)
                {
                    return true;
                }
                bool personExists = SelectedPersons.Contains(person);

                return !personExists;
            }
            return true;
        }
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            tasks.Add(LoadPersonsAsync(_data));
            tasks.Add(LoadSelectedPersonsAsync());

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
        private Task LoadSelectedPersonsAsync()
        {
            _selectedPersons.Clear();
            foreach (Person item in _selectedMachine.Responsibles)
            {
                _selectedPersons.Add(item);
            }
            OnPropertyChanged(nameof(SelectedPersons));
            return Task.CompletedTask;
        }
    }
}