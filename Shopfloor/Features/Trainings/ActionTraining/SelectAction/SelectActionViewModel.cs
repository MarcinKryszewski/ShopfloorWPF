using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Features.Trainings.ActionTraining.Contexts;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Utilities.Collections;

namespace Shopfloor.Features.Trainings.ActionTraining.SelectAction
{
    internal class SelectActionViewModel : ViewModelBase
    {
        private readonly List<Line> _lines = [];
        private readonly List<Machine> _machines = [];
        private readonly SelectedActionContext _actionContext;
        public SelectActionViewModel(SelectedActionContext actionContext)
        {
            _actionContext = actionContext;
            Machines = new ListCollectionView(_machines)
            {
                Filter = Filter,
            };

            ChooseActionCommand = new RelayCommand(ChooseAction, x => true);
        }
        public ConcurrentObservableCollection<Activity> Data { get; private set; } = [];
        public ICollectionView Activities => CollectionViewSource.GetDefaultView(Data.AsObservable);
        public ICommand ChooseActionCommand { get; }
        public Line? SelectedLine { get; set; }
        public Machine? SelectedMachine { get; set; }
        public ICollectionView Machines { get; private set; }
        public ICollectionView Lines => CollectionViewSource.GetDefaultView(_lines);
        private bool Filter(object obj)
        {
            if (obj is Machine machine && SelectedLine is not null)
            {
                bool line =
                    string.IsNullOrEmpty(SelectedLine.Name) ||
                    machine.Line!.Name.Contains(SelectedLine.Name, StringComparison.InvariantCultureIgnoreCase);

                return line;
            }
            return false;
        }
        private void ChooseAction(object parameter)
        {
            if (parameter is not Activity)
            {
                return;
            }
            _actionContext.Activity = (Activity)parameter;
        }
    }
}