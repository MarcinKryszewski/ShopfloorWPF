using System.ComponentModel;
using Shopfloor.Features.Trainings.ActionTraining.Contexts;
using Shopfloor.Features.Trainings.ActionTraining.SelectAction;
using Shopfloor.Features.Trainings.ActionTraining.SelectPerson;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Trainings.ActionTraining
{
    internal class ActionTrainingViewModel : ViewModelBase
    {
        private readonly SelectActionViewModel _actionViewModel;
        private readonly SelectPersonViewModel _personViewModel;
        private readonly SelectedActionContext _actionContext;
        private ViewModelBase _contentViewModel;
        public ActionTrainingViewModel(
            SelectActionViewModel actionViewModel,
            SelectPersonViewModel personViewModel,
            SelectedActionContext actionContext)
        {
            _actionViewModel = actionViewModel;
            _personViewModel = personViewModel;
            _actionContext = actionContext;

            _contentViewModel = _actionViewModel;
            _actionContext.PropertyChanged += OnActionContextChanged;
        }
        public ViewModelBase ContentViewModel => _contentViewModel;
        private void OnActionContextChanged(object? sender, PropertyChangedEventArgs e)
        {
            _contentViewModel = _actionContext.Activity is null ? _actionViewModel : _personViewModel;
            OnPropertyChanged(nameof(ContentViewModel));
        }
    }
}