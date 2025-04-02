using Shopfloor.Features.Trainings.ActionTraining;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Trainings.TrainingCreate
{
    internal class TrainingCreateViewModel : ViewModelBase
    {
        private readonly ActionTrainingViewModel _contentViewModel;
        public TrainingCreateViewModel(ActionTrainingViewModel actionTrainingViewModel)
        {
            _contentViewModel = actionTrainingViewModel;
        }
        public ActionTrainingViewModel ContentViewModel => _contentViewModel;
    }
}