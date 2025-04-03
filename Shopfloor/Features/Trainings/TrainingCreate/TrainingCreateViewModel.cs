using System.Windows.Input;
using Shopfloor.Features.Trainings.ActionTraining;
using Shopfloor.Features.Trainings.PersonTraining;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Trainings.TrainingCreate
{
    internal class TrainingCreateViewModel : ViewModelBase
    {
        private readonly ActionTrainingViewModel _actionTrainingViewModel;
        private readonly PersonTrainingViewModel _personTrainingViewModel;
        private ViewModelBase? _contentViewModel;
        public TrainingCreateViewModel(
            ActionTrainingViewModel actionTrainingViewModel,
            PersonTrainingViewModel personTrainingViewModel)
        {
            _actionTrainingViewModel = actionTrainingViewModel;
            _personTrainingViewModel = personTrainingViewModel;

            SetTrainingViewModelCommand = new RelayCommand(SetViewModel, x => true);
        }
        public ViewModelBase? ContentViewModel => _contentViewModel;
        public ICommand SetTrainingViewModelCommand { get; }
        private void SetViewModel(object parameter)
        {
            ViewModelBase? currentVm = _contentViewModel;

            _contentViewModel = parameter switch
            {
                "action" => _actionTrainingViewModel,
                "person" => _personTrainingViewModel,
                _ => null,
            };

            if (currentVm != _contentViewModel)
            {
                OnPropertyChanged(nameof(ContentViewModel));
            }
        }
    }
}