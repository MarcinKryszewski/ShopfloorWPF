using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.ActionEdit;
using Shopfloor.Features.Actions.ActionsList;
using Shopfloor.Features.TrainingsList;
using Shopfloor.Models.Activities;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.ActionDetails
{
    internal class ActionDetailsViewModel : ViewModelBase
    {
        private readonly ActivityContext _activityContext;
        private readonly TrainingsListViewModel _trainings;

        public ActionDetailsViewModel(
            ActivityContext activityContext,
            TrainingsListViewModel trainings,
            ViewModelBaseDependecies dependecies)
        : base(dependecies)
        {
            _activityContext = activityContext;
            ReturnCommand = new NavigationCommand<ActionsListViewModel>(NavigationService).Navigate();

            if (_activityContext.Activity is null)
            {
                ReturnCommand.Execute(null);
            }

            _activityContext.IsEditable = false;
            _trainings = trainings;
            EditCommand = new NavigationCommand<ActionEditViewModel>(NavigationService).Navigate();
        }
        public Activity Activity => _activityContext.Activity!;
        public ICommand EditCommand { get; }
        public ICommand ReturnCommand { get; }
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
        public TrainingsListViewModel Trainings => _trainings;
    }
}