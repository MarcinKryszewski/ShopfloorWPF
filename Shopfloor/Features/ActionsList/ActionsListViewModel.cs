using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Machines;
using Shopfloor.Roots;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.ActionsList
{
    internal class ActionsListViewModel : ViewModelBase
    {
        private readonly ActivitiesRoot _root;
        private readonly List<Activity> _activities = [];
        public ActionsListViewModel(
            ActivitiesRoot root,
            ActionsFilter filter)
        {
            _root = root;
            FilterData = filter;

            _root.DataChanged += DataChanged;
            FilterData.FiltersChanged += OnFiltersChanged;
            Activities.Filter = Filter;

            _ = LoadDataAsync();
        }
        public ICollectionView Activities => CollectionViewSource.GetDefaultView(_activities);
        public ActionsFilter FilterData { get; }
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