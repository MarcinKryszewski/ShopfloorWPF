using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Threading.Tasks;
using System.Windows.Input;
using Shopfloor.Features.Admin.Machines.Commands;
using System.Linq;

namespace Shopfloor.Features.Admin.Machines.List
{
    public class MachinesListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Machine> _machines;
        private readonly ObservableCollection<Machine> _machinesAll;
        private readonly IServiceProvider _database;
        private string _machineName = string.Empty;
        private string _machineNumber = string.Empty;
        private int _parentId;
        private Machine? _selectedParent;
        private Machine? _selectedMachine;
        private string _machineSearchText = string.Empty;

        public IEnumerable<Machine> Machines => _machines;
        public ICollectionView MachinesList => CollectionViewSource.GetDefaultView(_machinesAll);

        public bool IsEdit { get; set; }
        public string MachineName
        {
            get => _machineName;
            set
            {
                _machineName = value;
                OnPropertyChanged(nameof(MachineName));
            }
        }
        public string MachineNumber
        {
            get => _machineNumber;
            set
            {
                _machineNumber = value;
                OnPropertyChanged(nameof(MachineNumber));
            }
        }
        public int ParentId
        {
            get => _parentId;
            set
            {
                _parentId = value;
                OnPropertyChanged(nameof(ParentId));
            }
        }
        public Machine? SelectedParent
        {
            get => _selectedParent;
            set
            {
                _selectedParent = value;
                OnPropertyChanged(nameof(SelectedParent));
            }
        }
        public Machine? SelectedMachine
        {
            get => _selectedMachine;
            set
            {
                _selectedMachine = value;
                OnPropertyChanged(nameof(SelectedMachine));
            }
        }

        public string MachineSearchText
        {
            get => _machineSearchText;
            set
            {
                _machineSearchText = value;
                MachinesList.Filter = FilterMachines;
                OnPropertyChanged(nameof(MachineSearchText));
            }
        }

        public ICommand MachineDeleteCommand { get; }
        public ICommand MachineAddCommand { get; }
        public ICommand MachineEditCommand { get; }
        public ICommand MachineSetParentCommand { get; }
        public ICommand MachineSetCurrentCommand { get; }
        public ICommand CleanCommand { get; }
        public ICommand MachineSelectedCommand { get; }


        public MachinesListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _database = databaseServices;

            _machines = new();
            _machinesAll = new();
            _ = LoadMachines();
            IsEdit = false;

            MachineProvider provider = databaseServices.GetRequiredService<MachineProvider>();

            MachineDeleteCommand = new MachineDeleteCommand();
            MachineAddCommand = new MachineAddCommand(this, provider);
            MachineEditCommand = new MachineEditCommand(this, provider);
            MachineSetParentCommand = new MachineSetParentCommand();
            MachineSetCurrentCommand = new MachineSetCurrentCommand();
            CleanCommand = new CleanFormCommand();
            MachineSelectedCommand = new MachineSelectedCommand();
        }

        private async Task LoadMachines()
        {
            _machines.Clear();
            _machinesAll.Clear();
            IEnumerable<Machine> machines = await _database.GetRequiredService<MachineProvider>().GetAll();

            foreach (Machine machine in machines)
            {
                _machinesAll.Add(machine);

                if (machine.ParentId is null)
                {
                    AddRoot(machine);
                    continue;
                }

                AddChild(machine, _machinesAll);
            }
            //OnPropertyChanged(nameof(Machines));
        }

        private void AddRoot(Machine machine)
        {
            _machines.Add(machine);
        }
        private static void AddChild(Machine machine, ObservableCollection<Machine> machinesList)
        {
            Machine? machineParent = machinesList.FirstOrDefault(m => m.Id == machine.ParentId);
            if (machineParent is null) return;
            machineParent.AddChild(machine);
        }

        private bool FilterMachines(object obj)
        {
            if (obj is Machine machine)
            {
                return machine.Path.Contains(_machineSearchText, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        public async Task? UpdateMachines()
        {
            await LoadMachines();
            OnPropertyChanged(nameof(Machines));
        }
    }
}
