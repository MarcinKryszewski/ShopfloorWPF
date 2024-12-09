using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.ActionDetails;
using Shopfloor.Features.ActionEdit.Commands;
using Shopfloor.Features.ActionsList;
using Shopfloor.Features.TrainingsList;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Persons;
using Shopfloor.Roots;
using Shopfloor.Services.AuthServices;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.ActionEdit
{
    internal class ActionEditViewModel : ViewModelBase
    {
        private readonly ActivityContext _activityContext;
        private readonly DataRoot _data;
        private readonly TrainingsListViewModel _trainings;
        private readonly IUserContext _userContext;
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
            _userContext = dependecies.UserContext;
            _data = data;
            _trainings = trainings;

            CancelCommand = new NavigationCommand<ActionDetailsViewModel>(NavigationService).Navigate();
            SaveCommand = new ActionEditCommand();
        }
        public Activity Activity => _activityContext.Activity!;
        public ICommand CancelCommand { get; }
        public Person? CurrentPerson => _userContext.Person;
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
        public TrainingsListViewModel Trainings => _trainings;
    }
}