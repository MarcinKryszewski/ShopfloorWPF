using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.ActionDetails;
using Shopfloor.Features.ActionEdit.Commands;
using Shopfloor.Features.ActionsList;
using Shopfloor.Features.TrainingsList;
using Shopfloor.Models.Activities;
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
        private readonly ActivityContext _activityContext;
        private readonly DataRoot _data;
        private readonly TrainingsListViewModel _trainings;
        private readonly IUserContext _userContext;
        private readonly List<Workshop> _workshops = [];
        private readonly Activity _activity;
        public ActionEditViewModel(
            ActivityContext activityContext,
            ViewModelBaseDependecies dependecies,
            DataRoot data,
            TrainingsListViewModel trainings)
        : base(dependecies)
        {
            _activityContext = activityContext;
            ReturnCommand = new NavigationCommand<ActionsListViewModel>(NavigationService).Navigate();

            if (_activityContext.Activity is null)
            {
                ReturnCommand.Execute(null);
            }

            _activityContext.IsEditable = true;
            _activity = _activityContext.Activity!.Clone();
            _userContext = dependecies.UserContext;
            _data = data;
            _trainings = trainings;

            CancelCommand = new NavigationCommand<ActionDetailsViewModel>(NavigationService).Navigate();
            SaveCommand = new ActionEditCommand();

            _ = LoadDataAsync();
        }
        public Activity Activity => _activity;
        public ICommand CancelCommand { get; }
        public Person? CurrentPerson => _userContext.Person;
        public ICommand ReturnCommand { get; }
        public ICommand SaveCommand { get; }
        public ICollectionView Workshops => CollectionViewSource.GetDefaultView(_workshops);
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

            tasks.Add(LoadWorkshops());

            await Task.WhenAll(tasks);
        }
        public TrainingsListViewModel Trainings => _trainings;
        private async Task LoadWorkshops()
        {
            IEnumerable<Workshop> data = await _data.GetWorkshop();
            await BatchListUpdater.UpdateAsync(data, _workshops, Workshops);
        }
    }
}