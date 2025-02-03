using System.Windows.Input;
using Shopfloor.Features.Actions.ActionsList;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Trainings.ActionTrainingList
{
    internal class ActionTrainingListViewModel : ViewModelBase
    {
        public ActionTrainingListViewModel(ViewModelBaseDependecies dependecies)
                : base(dependecies)
        {
        }
        public ICommand ReturnCommand => new NavigationCommand<ActionsListViewModel>(NavigationService).Navigate();
    }
}