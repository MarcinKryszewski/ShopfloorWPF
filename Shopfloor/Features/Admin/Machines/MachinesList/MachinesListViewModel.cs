using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Shopfloor.Features.Admin.Machines.List
{
    public class MachinesListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Machine> _machines;
        private IServiceProvider _database;

        public IEnumerable<Machine> Machines => _machines;

        public MachinesListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _database = databaseServices;

            _machines = new();
            _ = LoadMachines();
        }

        private async Task LoadMachines()
        {
            _machines.Clear();
            IEnumerable<Machine> machines = await _database.GetRequiredService<MachineProvider>().GetAll();
            List<Machine> machinesList = new();

            foreach (Machine machine in machines)
            {
                machinesList.Add(machine);

                if (machine.ParentId is null)
                {
                    AddRoot(machine);
                    continue;
                }

                AddChild(machine, machinesList);
            }
            //OnPropertyChanged(nameof(Machines));
        }

        private void AddRoot(Machine machine)
        {
            _machines.Add(machine);
        }
        private static void AddChild(Machine machine, List<Machine> machinesList)
        {
            Machine? machineParent = machinesList.Find(m => m.Id == machine.ParentId);
            if (machineParent is null) return;
            machineParent.AddChild(machine);
        }
    }
}
