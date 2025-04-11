using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Features.Trainings.ActionTraining.Contexts;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Roots;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Utilities.Collections;

namespace Shopfloor.Features.Trainings.ActionTraining.SelectAction
{
    internal class SelectActionViewModel : ViewModelBase
    {
        private readonly List<Line> _lines = [];
        private readonly List<Machine> _machines = [];
        private readonly SelectedActionContext _actionContext;
        private readonly ActivitiesRoot _activitiesData;
        private readonly ActivitiesDataRoot _activitiesDataRoot;
        private Line? _selectedLine;
        private Machine? _selectedMachine;
        public SelectActionViewModel(
            SelectedActionContext actionContext,
            ActivitiesRoot activitiesRoot,
            ActivitiesDataRoot activitiesDataRoot)
        {
            _actionContext = actionContext;
            _activitiesData = activitiesRoot;
            _activitiesDataRoot = activitiesDataRoot;

            Machines = new ListCollectionView(_machines)
            {
                Filter = FilterMachines,
            };
            Activities = CollectionViewSource.GetDefaultView(_activitiesData.Data.AsObservable);
            Activities.Filter = FilterActions;

            ChooseActionCommand = new RelayCommand(ChooseAction, x => true);
            Task.Run(LoadDataAsync);
        }
        public ICollectionView Activities { get; }
        public ICommand ChooseActionCommand { get; }
        public Line? SelectedLine
        {
            get => _selectedLine;
            set
            {
                string machineName = SelectedMachine?.Name ?? string.Empty;
                _selectedLine = value;
                if (_selectedLine is null)
                {
                    return;
                }
                if (!CollectionHelper.IsInIEnumerable<Line>(_selectedLine.Name, Lines))
                {
                    // SelectedLine = null;
                    OnPropertyChanged(nameof(SelectedLine));
                }
                Machines.Refresh();
                if (!CollectionHelper.IsInIEnumerable<Machine>(machineName, Machines))
                {
                    SelectedMachine = null;
                    OnPropertyChanged(nameof(SelectedMachine));
                }
                OnPropertyChanged(nameof(SelectedLine));
                // OnFiltersChanged(EventArgs.Empty);
            }
        }
        public Machine? SelectedMachine
        {
            get => _selectedMachine;
            set
            {
                _selectedMachine = value;
                Activities.Refresh();
                OnPropertyChanged(nameof(_selectedMachine));
            }
        }
        public ICollectionView Machines { get; private set; }
        public ICollectionView Lines => CollectionViewSource.GetDefaultView(_lines);
        private bool FilterMachines(object obj)
        {
            if (obj is Machine machine && SelectedLine is not null)
            {
                bool line =
                    string.IsNullOrEmpty(SelectedLine.Name) ||
                    machine.Line!.Name.Contains(SelectedLine.Name, StringComparison.InvariantCultureIgnoreCase);
                OnPropertyChanged(nameof(Machines));

                return line;
            }
            return false;
        }
        private bool FilterActions(object obj)
        {
            if (SelectedMachine is null)
            {
                return true;
            }

            if (obj is Activity activity)
            {
                bool onTheList = activity.MachineId == SelectedMachine.Id;
                return onTheList;
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
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            await _activitiesData.GetData();
            _lines.AddRange(await _activitiesDataRoot.GetLines());
            _machines.AddRange(await _activitiesDataRoot.GetMachines());

            OnPropertyChanged(nameof(Lines));
            OnPropertyChanged(nameof(Machines));

            await Task.WhenAll(tasks);
        }
    }
}