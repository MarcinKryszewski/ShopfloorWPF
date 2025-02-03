using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.ActionDetails;
using Shopfloor.Features.ActionEdit.Commands;
using Shopfloor.Features.Actions.ActionsList;
using Shopfloor.Features.TrainingsList;
using Shopfloor.Models.Activities;
using Shopfloor.Models.ActivityTypes;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Workshops;
using Shopfloor.Roots;
using Shopfloor.Services.AuthServices;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.ActionEdit
{
    internal class ActionEditViewModel : ViewModelBase
    {
        private readonly ActivityCreation _activity;
        private readonly ActivityContext _activityContext;
        private readonly List<ActivityType> _activityTypes = [];
        private readonly DataRoot _data;
        private readonly IUserContext _userContext;
        private readonly List<Workshop> _workshops = [];
        public ActionEditViewModel(
            ActivityContext activityContext,
            ViewModelBaseDependecies dependecies,
            DataRoot data,
            ActivitiesRoot activitiesRoot,
            TrainingsListViewModel trainings)
        : base(dependecies)
        {
            _activityContext = activityContext;
            Activity? activity = _activityContext.Activity;
            ReturnCommand = new NavigationCommand<ActionsListViewModel>(NavigationService).Navigate();

            if (activity is null)
            {
                ReturnCommand.Execute(null);
            }

            _activityContext.IsEditable = true;
            _activity = new(activity!);

            _userContext = dependecies.UserContext;
            _data = data;

            Trainings = trainings;

            CancelCommand = new NavigationCommand<ActionDetailsViewModel>(NavigationService).Navigate();
            SaveCommand = new ActionEditCommand(activitiesRoot);

            _ = LoadDataAsync();
        }
        public ActivityCreation Activity => _activity;
        public ICollectionView ActivityTypes => CollectionViewSource.GetDefaultView(_activityTypes);
        public ICommand CancelCommand { get; }
        public Person? CurrentPerson => _userContext.Person;
        public ICollectionView OccuranceUnits { get; private set; } = new ListCollectionView(new List<Occurance>());
        public ICommand ReturnCommand { get; }
        public ICommand SaveCommand { get; }
        public string Title
        {
            get
            {
                string type = Activity.Type is null ? string.Empty : Activity.Type.Name;
                string line = Activity.Machine?.Line?.Name ?? string.Empty;
                string machine = Activity.Machine?.Name ?? string.Empty;
                return $"EDYCJA {type} - {line} - {machine}";
            }
        }
        public TrainingsListViewModel Trainings { get; }
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            tasks.Add(LoadTypes());
            tasks.Add(LoadOccuranceUnits());

            await Task.WhenAll(tasks);
            OnPropertyChanged(nameof(Activity));
        }
        private Task LoadOccuranceUnits()
        {
            List<Occurance> occurances = [];
            foreach (OccuranceUnit item in Enum.GetValues(typeof(OccuranceUnit)))
            {
                occurances.Add(new()
                {
                    Unit = item,
                });
            }
            OccuranceUnits = new ListCollectionView(occurances);
            return Task.CompletedTask;
        }
        private async Task LoadTypes()
        {
            IEnumerable<ActivityType> data = await _data.GetActivityType();
            await BatchListUpdater.UpdateAsync(data, _activityTypes);
        }
    }
}