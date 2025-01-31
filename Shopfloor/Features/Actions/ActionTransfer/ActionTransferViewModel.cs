using System.Windows.Input;
using Shopfloor.Features.ActionsList;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Action.ActionTransfer
{
    internal class ActionTransferViewModel : ViewModelBase
    {
        public ActionTransferViewModel(ViewModelBaseDependecies dependecies)
        : base(dependecies)
        {
        }
        public ICommand ReturnCommand => new NavigationCommand<ActionsListViewModel>(NavigationService).Navigate();
    }
}