using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.ActionCreate;
using Shopfloor.Features.ActionDetails;
using Shopfloor.Features.ActionEdit;
using Shopfloor.Models.Activities;
using Shopfloor.Roots;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.ActionsList
{
    internal class ActionsListViewModel : ViewModelBase
    {
        private static readonly object _syncLock = new();
        private readonly List<Activity> _activities = [];
        private readonly ActivityContext _activityContext;
        private readonly ActivitiesRoot _root;
        public ActionsListViewModel(
            ActivitiesRoot root,
            ActionsFilter filter,
            ActivityContext activityContext,
            ViewModelBaseDependecies dependecies)
        : base(dependecies)
        {
            _root = root;
            FilterData = filter;
            _activityContext = activityContext;
            Activities = new ListCollectionView(_activities)
            {
                Filter = Filter,
            };
            _root.DataChanged += DataChanged;
            FilterData.FiltersChanged += OnFiltersChanged;
            BindingOperations.EnableCollectionSynchronization(_activities, _syncLock);

            DetailsCommand = new NavigationCommand<ActionDetailsViewModel>(NavigationService).Navigate();
            EditCommand = new NavigationCommand<ActionEditViewModel>(NavigationService).Navigate();
            CreateActionCommand = new NavigationCommand<ActionCreateViewModel>(NavigationService).Navigate();

            Task.Run(LoadDataAsync);
        }
        public ICollectionView Activities { get; init; }
        public Activity? Activity
        {
            get => _activityContext.Activity;
            set => _activityContext.Activity = value;
        }
        public ICommand CreateActionCommand { get; }
        public ICommand DetailsCommand { get; }
        public ICommand EditCommand { get; }
        public ActionsFilter FilterData { get; }
        public void DataChanged(object? sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                lock (_syncLock)
                {
                    OnPropertyChanged(nameof(Activities));
                    Activities.Refresh();
                }
            });
        }
        public void OnFiltersChanged(object? sender, EventArgs e)
        {
            Activities.Refresh();
        }
        private bool Filter(object obj)
        {
            if (obj is Activity activity)
            {
                bool workshop = string.IsNullOrEmpty(FilterData.Workshop) || activity.Workshop!.Name.Contains(FilterData.Workshop, StringComparison.InvariantCultureIgnoreCase);
                bool line = string.IsNullOrEmpty(FilterData.Line) || activity.Machine!.Line!.Name.Contains(FilterData.Line, StringComparison.InvariantCultureIgnoreCase);
                bool machine = string.IsNullOrEmpty(FilterData.Machine) || activity.Machine!.Name.Contains(FilterData.Machine, StringComparison.InvariantCultureIgnoreCase);
                bool type = string.IsNullOrEmpty(FilterData.Type) || activity.Type!.Name.Contains(FilterData.Type, StringComparison.InvariantCultureIgnoreCase);
                bool isPassed = FilterData.IsPassed == null || (bool)FilterData.IsPassed == activity.IsPassed;
                bool isLoto = !FilterData.IsLoto || activity.IsLoto;
                bool isJog = !FilterData.IsJog || activity.IsJog;
                bool isProduction = !FilterData.IsProduction || activity.IsDurningProduction;
                bool hasInstruction = FilterData.HasInstruction == null || (bool)FilterData.HasInstruction == activity.HasInstruction;

                return workshop && line && machine && type && isPassed && isLoto && isJog && isProduction && hasInstruction;
            }
            return false;
        }
        private async Task LoadActivitiesAsync()
        {
            IEnumerable<Activity> activities = await _root.GetData();
            BatchListUpdater.DataChanged += DataChanged;
            await BatchListUpdater.UpdateAsync(activities, _activities);
            BatchListUpdater.DataChanged -= DataChanged;
        }
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            tasks.Add(LoadActivitiesAsync());

            await Task.WhenAll(tasks);
        }
    }
}