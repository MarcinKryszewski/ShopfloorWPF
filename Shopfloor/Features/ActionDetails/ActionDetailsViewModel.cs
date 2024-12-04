using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.ActionsList;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Trainings;
using Shopfloor.Roots;
using Shopfloor.Services.AuthServices;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.ActionDetails
{
    internal class ActionDetailsViewModel : ViewModelBase
    {
        private readonly ActivityContext _activityContext;
        private readonly List<Training> _trainings = [];
        private readonly TrainingsRoot _trainingsRoot;
        private readonly IUserContext _userContext;

        public ActionDetailsViewModel(
            ActivityContext activityContext,
            TrainingsRoot trainingsRoot,
            ViewModelBaseDependecies dependecies,
            IUserContext userContext)
        : base(dependecies)
        {
            _activityContext = activityContext;
            _trainingsRoot = trainingsRoot;
            _userContext = userContext;
            ReturnCommand = new NavigationCommand<ActionsListViewModel>(NavigationService).Navigate();

            if (_activityContext.Activity is null)
            {
                ReturnCommand.Execute(null);
            }

            TestList.Add(Activity);

            _ = LoadDataAsync();
        }
        public Activity Activity => _activityContext.Activity!;
        public Person? CurrentPerson => _userContext.Person;
        public ICommand ReturnCommand { get; }
        public List<Activity> TestList { get; } = [];
        public string Title
        {
            get
            {
                string type = Activity.Type is null ? string.Empty : Activity.Type.Name;
                string line = Activity.Machine?.Line?.Name ?? string.Empty;
                string machine = Activity.Machine?.Name ?? string.Empty;
                return $"{type} - {line} - {machine}";
            }
        }
        public ICollectionView Trainings => CollectionViewSource.GetDefaultView(_trainings);
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