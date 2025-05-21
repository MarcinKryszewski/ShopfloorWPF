using System.Windows.Input;
using Shopfloor.Features.Actions.ActionsList;
using Shopfloor.Features.Responsibilities.MachineResponsibilities;
using Shopfloor.Features.Trainings.ListTrainee;
using Shopfloor.Features.Trainings.ListTrainer;
using Shopfloor.Features.Trainings.TrainingDetails;
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

            NavigateListTrainee = new NavigationCommand<ListTraineeViewModel>(navigationService).Navigate();
            // NavigateListTrainer = new NavigationCommand<ListTrainerViewModel>(navigationService).Navigate();
            NavigateListTrainer = new NavigationCommand<TrainingDetailsViewModel>(navigationService).Navigate();
        }
        public ICommand NavigateActionsList { get; }
        public ICommand NavigateMachineResponsibilities { get; }
        public ICommand NavigateListTrainee { get; }
        public ICommand NavigateListTrainer { get; }
    }
}