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
using Shopfloor.Features.Responsibilities.MachineResponsibilityEdit;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Workshops;
using Shopfloor.Roots;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Responsibilities.MachineResponsibilities
{
    internal class MachineResponsibilitiesViewModel : ViewModelBase
    {
        private static readonly object _syncLock = new();
        private readonly MachineContext _context;
        private readonly IDataRoot _data;
        private readonly MachinesRoot _machinesRoot;
        private string _lineName = string.Empty;

        public MachineResponsibilitiesViewModel(
            MachinesRoot machinesRoot,
            MachineContext context,
            ViewModelBaseDependecies dependecies,
            IDataRoot data)
            : base(dependecies)
        {
            _machinesRoot = machinesRoot;
            _context = context;
            _data = data;

            _machinesRoot.DataChanged += OnDataChanged;

            Machines.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Machine.LineId)));
            Machines.Filter = Filter;

            EditResponsibilitiesCommand = new NavigationCommand<MachineResponsibilityEditViewModel>(NavigationService).Navigate();

            Task.Run(LoadDataAsync);
        }
        public ICommand EditResponsibilitiesCommand { get; }
        public string LineName
        {
            get => _lineName;
            set
            {
                _lineName = value;
                Machines.Refresh();
            }
        }
        public ICollectionView Lines { get; private set; } = new ListCollectionView(new List<Line>());
        public ICollectionView Machines => CollectionViewSource.GetDefaultView(_machinesRoot.Data.AsObservable);
        public string MissingWorkshops
        {
            get
            {
                if (SelectedMachine is null)
                {
                    return string.Empty;
                }

                IEnumerable<Workshop> workshopsData = _data.GetWorkshops().Result;
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
        public ICollectionView Persons { get; private set; } = new ListCollectionView(new List<Person>());
        public Line? SelectedLine { get; set; }
        public Machine? SelectedMachine
        {
            get => _context.Machine;
            set
            {
                _context.Machine = value;
                OnPropertyChanged(nameof(SelectedMachine));
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(MissingWorkshops));
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
        public void OnDataChanged(object? sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                lock (_syncLock)
                {
                    OnPropertyChanged(nameof(Machines));
                    Machines.Refresh();
                }
            });
        }
        private static async Task LoadMachinesAsync(MachinesRoot dataRoot)
        {
            await dataRoot.GetData();
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
        private bool Filter(object obj)
        {
            if (obj is Machine machine)
            {
                bool line =
                    string.IsNullOrEmpty(LineName) ||
                    machine.Line!.Name.Contains(LineName, StringComparison.InvariantCultureIgnoreCase);

                return line;
            }
            return false;
        }
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            tasks.Add(LoadMachinesAsync(_machinesRoot));
            tasks.Add(LoadLinesAsync(_data));
            tasks.Add(LoadPersonsAsync(_data));

            await Task.WhenAll(tasks);
        }
        private async Task LoadLinesAsync(IDataRoot dataRoot)
        {
            List<Line> data = (await dataRoot.GetLines()).ToList();
            Lines = new ListCollectionView(data);
            OnPropertyChanged(nameof(Lines));
        }
        private async Task LoadPersonsAsync(IDataRoot dataRoot)
        {
            List<Person> data = (await dataRoot.GetPersons()).ToList();
            Persons = new ListCollectionView(data);
            OnPropertyChanged(nameof(Persons));
        }
    }
}