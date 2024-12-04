using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.ActionsList;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Trainings;
using Shopfloor.Roots;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.ActionEdit
{
    internal class ActionEditViewModel : ViewModelBase
    {
        private readonly ActivityContext _activityContext;
        private readonly TrainingsRoot _trainingsRoot;
        private readonly List<Training> _trainings = [];
        public ActionEditViewModel(
            ActivityContext activityContext,
            TrainingsRoot trainingsRoot,
            ViewModelBaseDependecies dependecies)
        : base(dependecies)
        {
            _activityContext = activityContext;
            _trainingsRoot = trainingsRoot;
            ReturnCommand = new NavigationCommand<ActionsListViewModel>(NavigationService).Navigate();

            if (_activityContext.Activity is null)
            {
                ReturnCommand.Execute(null);
            }

            TestList.Add(Activity);

            _ = LoadDataAsync();
        }
        public ICollectionView Trainings => CollectionViewSource.GetDefaultView(_trainings);
        public ICommand ReturnCommand { get; }
        public bool IsViewedByTrainee => false;
        public bool IsViewedByCoach => true;
        public Activity Activity => _activityContext.Activity!;
        public List<Activity> TestList { get; } = [];
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
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            tasks.Add(LoadTrainingsAsync());

            await Task.WhenAll(tasks);
        }
        private async Task LoadTrainingsAsync()
        {
            IEnumerable<Training> data = await _trainingsRoot.GetData();
            await BatchListUpdater.UpdateAsync(data.Where(x => x.ActivityId == Activity.Id), _trainings, Trainings);
        }
    }
}