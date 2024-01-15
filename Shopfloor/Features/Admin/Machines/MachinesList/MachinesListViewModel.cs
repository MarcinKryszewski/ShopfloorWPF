using Shopfloor.Models;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shopfloor.Features.Admin.Machines.List
{
    public class MachinesListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Machine> _machines;
        public IEnumerable<Machine> Machines => _machines;
        public MachinesListViewModel(IServiceProvider mainServices, IServiceProvider databasServices)
        {
            //TODO: LoadData, add to parents, add to _machines if parent is null
            Machine machine1 = new(1, "RB1", "AAA", null);
            Machine machine2 = new(2, "Monoblok", "AAA", 1);
            Machine machine3 = new(3, "RB2", "4324", null);
            machine1.Children.Add(machine2);
            _machines = new ObservableCollection<Machine>
            {
                machine1,
                machine3
            };
        }
    }
}
