using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using Shopfloor.Models.Activities;
using Shopfloor.Roots;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.ActionsList
{
    internal class ActionsListViewModel : ViewModelBase
    {
        private readonly ActivitiesRoot _root;
        private readonly List<Activity> _activities = [];
        public ActionsListViewModel(ActivitiesRoot root)
        {
            _root = root;
            _root.DataChanged += DataChanged;
            _ = LoadDataAsync();
        }
        public ICollectionView Activities => CollectionViewSource.GetDefaultView(_activities);
        public void DataChanged(object? sender, EventArgs e)
        {
            Activities.Refresh();
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