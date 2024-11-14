using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.ActionsList;
using Shopfloor.Models.Activities;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.ActionDetails
{
    internal class ActionDetailsViewModel : ViewModelBase
    {
        private readonly ActivityContext _activityContext;
        public ActionDetailsViewModel(
            ActivityContext activityContext,
            ViewModelBaseDependecies dependecies)
        : base(dependecies)
        {
            _activityContext = activityContext;
            ReturnCommand = new NavigationCommand<ActionsListViewModel>(NavigationService).Navigate();

            if (_activityContext.Activity is null)
            {
                ReturnCommand.Execute(null);
            }
        }
        public ICommand ReturnCommand { get; }
        public Activity Activity => _activityContext.Activity!;
    }
}