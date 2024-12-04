using System.Windows.Input;
using Shopfloor.Features.ActionsList;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.ActionCreate
{
    internal class ActionCreateViewModel : ViewModelBase
    {
        public ActionCreateViewModel(ViewModelBaseDependecies dependecies)
        : base(dependecies)
        {
            ReturnCommand = new NavigationCommand<ActionsListViewModel>(NavigationService).Navigate();
        }
        public ICommand ReturnCommand { get; }
    }
}