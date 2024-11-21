using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.ActionDetails;
using Shopfloor.Models.Activities;
using Shopfloor.Roots;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.ActionsList
{
    internal class ActionsListViewModel : ViewModelBase
    {
        private readonly ActivitiesRoot _root;
        private readonly ActivityContext _activityContext;
        private readonly List<Activity> _activities = [];
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

            _root.DataChanged += DataChanged;
            FilterData.FiltersChanged += OnFiltersChanged;
            Activities.Filter = Filter;

            DetailsCommand = new NavigationCommand<ActionDetailsViewModel>(NavigationService).Navigate();

            _ = LoadDataAsync();
        }
        public ICollectionView Activities => CollectionViewSource.GetDefaultView(_activities);
        public ActionsFilter FilterData { get; }
        public ICommand DetailsCommand { get; }
        public Activity? Activity
        {
            get => _activityContext.Activity;
            set => _activityContext.Activity = value;
        }
        public void OnFiltersChanged(object? sender, EventArgs e)
        {
            Activities.Refresh();
        }
        public void DataChanged(object? sender, EventArgs e)
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
        private async Task LoadDataAsync()
        {
            // await Task.Delay(5000);
            List<Task> tasks = [];

            tasks.Add(LoadActivitiesAsync());

            await Task.WhenAll(tasks);
        }
        private async Task LoadActivitiesAsync()
        {
            IEnumerable<Activity> activities = await _root.GetData();
            await BatchListUpdater.UpdateAsync(activities, _activities, Activities);
        }
    }
}