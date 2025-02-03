using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Features.Actions.ActionCreate.Commands;
using Shopfloor.Features.Actions.ActionsList;
using Shopfloor.Models.Activities;
using Shopfloor.Models.ActivityTypes;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Workshops;
using Shopfloor.Roots;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Actions.ActionCreate
{
    internal class ActionCreateViewModel : ViewModelBase
    {
        private Line? _line;
        private string _lineName = string.Empty;
        private string _machineName = string.Empty;
        private string _typeName = string.Empty;
        private string _workshopName = string.Empty;
        public ActionCreateViewModel(
            ViewModelBaseDependecies dependecies,
            ActivitiesDataRoot dataRoot,
            ActivitiesRoot activitiesRoot)
        : base(dependecies)
        {
            ReturnCommand = new NavigationCommand<ActionsListViewModel>(NavigationService).Navigate();
            SaveCommand = new ActionCreateCommand(activitiesRoot);
            Task.Run(() => LoadDataAsync(dataRoot));

            SaveCommand.ExecuteFinished += OnActivitySave;
        }
        public ActivityCreation Activity { get; private set; } = new();
        public Line? Line
        {
            get => _line;
            set
            {
                Machine? machine = Activity.Machine;
                _line = value;
                Machines.Refresh();
                if (!Machines.Contains(machine))
                {
                    Activity.Machine = null;
                }
                OnPropertyChanged(nameof(Activity));
            }
        }
        public string LineName
        {
            get => _lineName;
            set
            {
                _lineName = value;
                if (!CollectionHelper.IsInIEnumerable<Line>(_lineName, Lines))
                {
                    Line = null;
                    OnPropertyChanged(nameof(Line));
                }
            }
        }
        public ICollectionView Lines { get; private set; } = new ListCollectionView(new List<Line>());
        public string MachineName
        {
            get => _machineName;
            set
            {
                _machineName = value;
                if (!CollectionHelper.IsInIEnumerable<Machine>(_machineName, Machines))
                {
                    Activity.Machine = null;
                    OnPropertyChanged(nameof(Activity));
                }
            }
        }
        public ICollectionView Machines { get; private set; } = new ListCollectionView(new List<Machine>());
        public ICollectionView Occurencies { get; private set; } = new ListCollectionView(new List<Occurance>());
        public ICommand ReturnCommand { get; }
        public ActionCreateCommand SaveCommand { get; }
        public string TypeName
        {
            get => _typeName;
            set
            {
                _typeName = value;
                if (!CollectionHelper.IsInIEnumerable<ActivityType>(_typeName, Types))
                {
                    Activity.Type = null;
                    OnPropertyChanged(nameof(Activity));
                }
            }
        }
        public ICollectionView Types { get; private set; } = new ListCollectionView(new List<ActivityType>());
        public string WorkshopName
        {
            get => _workshopName;
            set
            {
                _workshopName = value;
                if (!CollectionHelper.IsInIEnumerable<Workshop>(_workshopName, Workshops))
                {
                    Activity.Workshop = null;
                    OnPropertyChanged(nameof(Activity));
                }
            }
        }
        public ICollectionView Workshops { get; private set; } = new ListCollectionView(new List<Workshop>());
        public void OnActivitySave(object? sender, EventArgs e)
        {
            if (SaveCommand.ExecutedSuccessful)
            {
                Activity = new();
                Notifier.ShowSuccess(SaveCommand.NotifyText);
                Line = null;

                OnPropertyChanged(nameof(Activity));
                OnPropertyChanged(nameof(Line));

                return;
            }

            Notifier.ShowError(SaveCommand.NotifyText);
        }
        private bool Filter(object obj)
        {
            if (obj is Machine machine)
            {
                bool line = string.IsNullOrEmpty(Line?.Name ?? string.Empty) || machine.Line!.Name.Contains(Line?.Name ?? string.Empty, StringComparison.InvariantCultureIgnoreCase);

                return line;
            }
            return false;
        }
        private async Task LoadDataAsync(ActivitiesDataRoot dataRoot)
        {
            List<Task> tasks = [];

            tasks.Add(LoadLinesAsync(dataRoot));
            tasks.Add(LoadTypesAsync(dataRoot));
            tasks.Add(LoadWorkshopsAsync(dataRoot));
            tasks.Add(LoadMachinesAsync(dataRoot));
            tasks.Add(LoadOccurenciesAsync(dataRoot));

            await Task.WhenAll(tasks);
        }
        private async Task LoadLinesAsync(ActivitiesDataRoot dataRoot)
        {
            List<Line> data = await dataRoot.GetLines();
            Lines = new ListCollectionView(data);
            OnPropertyChanged(nameof(Lines));
        }
        private async Task LoadMachinesAsync(ActivitiesDataRoot dataRoot)
        {
            List<Machine> data = await dataRoot.GetMachines();
            Machines = new ListCollectionView(data)
            {
                Filter = Filter,
            };
            OnPropertyChanged(nameof(Types));
        }
        private async Task LoadOccurenciesAsync(ActivitiesDataRoot dataRoot)
        {
            List<Occurance> data = await dataRoot.GetOccurencies();
            Occurencies = new ListCollectionView(data);
            OnPropertyChanged(nameof(Types));
        }
        private async Task LoadTypesAsync(ActivitiesDataRoot dataRoot)
        {
            List<ActivityType> data = await dataRoot.GetTypes();
            Types = new ListCollectionView(data);
            OnPropertyChanged(nameof(Types));
        }
        private async Task LoadWorkshopsAsync(ActivitiesDataRoot dataRoot)
        {
            List<Workshop> data = await dataRoot.GetWorkshops();
            Workshops = new ListCollectionView(data);
            OnPropertyChanged(nameof(Types));
        }
    }
}