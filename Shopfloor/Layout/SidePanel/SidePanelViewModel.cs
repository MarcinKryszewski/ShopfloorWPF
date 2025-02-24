using System.Windows.Input;
using Shopfloor.Features.Actions.ActionsList;
using Shopfloor.Features.Responsibilities.MachineResponsibilities;
using Shopfloor.Features.Trainings.TrainingMain;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Layout.SidePanel
{
    internal sealed partial class SidePanelViewModel : ViewModelBase
    {
        public SidePanelViewModel(INavigationService navigationService)
        {
            NavigateActionsList = new NavigationCommand<ActionsListViewModel>(navigationService).Navigate();
            NavigateMachineResponsibilities = new NavigationCommand<MachineResponsibilitiesViewModel>(navigationService).Navigate();
            NavigateTrainings = new NavigationCommand<TrainingMainViewModel>(navigationService).Navigate();
        }
        public ICommand NavigateActionsList { get; }
        public ICommand NavigateMachineResponsibilities { get; }
        public ICommand NavigateTrainings { get; }
    }
}