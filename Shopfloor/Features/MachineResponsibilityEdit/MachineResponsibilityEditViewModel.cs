using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.MachineResponsibilities;
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

            Task.Run(LoadDataAsync);
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
        public ICollectionView Persons { get; private set; } = new ListCollectionView(new List<Person>());
        public ICommand ReturnCommand { get; }
        public Machine? SelectedMachine => _context.Machine;
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
            Persons = new ListCollectionView(data);
            OnPropertyChanged(nameof(Persons));
        }
    }
}