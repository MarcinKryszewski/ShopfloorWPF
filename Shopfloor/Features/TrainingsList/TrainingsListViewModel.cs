using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.ActionEdit.Commands;
using Shopfloor.Features.TrainingsList.Commands;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Trainings;
using Shopfloor.Roots;
using Shopfloor.Services.AuthServices;
using Shopfloor.Shared.HelperFunctions;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.TrainingsList
{
    internal class TrainingsListViewModel : ViewModelBase
    {
        private readonly ActivityContext _activityContext;
        private readonly List<Training> _trainings = [];
        private readonly TrainingsRoot _trainingsRoot;
        private readonly IUserContext _userContext;
        public TrainingsListViewModel(
            ActivityContext activityContext,
            ViewModelBaseDependecies dependecies,
            TrainingsRoot trainingsRoot)
        {
            _trainingsRoot = trainingsRoot;
            _userContext = dependecies.UserContext;
            _activityContext = activityContext;
            _activityContext.PropertyChanged += OnActionEditableChange;
            ConfirmTraining = new ConfirmTrainingCommand(_userContext, _trainingsRoot);
            _ = LoadTrainingsAsync();
        }
        public Activity? Activity => _activityContext.Activity;
        public ICommand ConfirmTraining { get; }
        public Person? CurrentPerson => _userContext.Person;
        public bool IsEditable => _activityContext.IsEditable;
        public ICollectionView Trainings => CollectionViewSource.GetDefaultView(_trainings);
        private async Task LoadTrainingsAsync()
        {
            if (Activity is null)
            {
                return;
            }

            IEnumerable<Training> data = (await _trainingsRoot.GetData())
                .Where(x => x.ActivityId == Activity.Id);

            await BatchListUpdater.UpdateAsync(data, _trainings, Trainings);
        }
        private void OnActionEditableChange(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_activityContext.IsEditable))
            {
                OnPropertyChanged(nameof(IsEditable));
            }
        }
    }
}