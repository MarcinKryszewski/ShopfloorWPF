using System;
using System.Windows.Input;
using Shopfloor.Features.ActionsList;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Layout.SidePanel
{
    internal sealed partial class SidePanelViewModel : ViewModelBase
    {
        public SidePanelViewModel(INavigationService navigationService)
        {
            NavigateActionsList = new NavigationCommand<ActionsListViewModel>(navigationService).Navigate();
        }
        public ICommand NavigateActionsList { get; }
    }
}