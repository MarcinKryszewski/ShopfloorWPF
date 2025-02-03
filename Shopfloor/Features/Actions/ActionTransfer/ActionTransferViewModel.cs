using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Shopfloor.Contexts;
using Shopfloor.Features.Actions.ActionsList;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.MachinesResponsibles;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Trainings;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Actions.ActionTransfer
{
    internal class ActionTransferViewModel : ViewModelBase
    {
        private readonly List<ResponsibleTraining> _responsibleTrainings = [];
        private readonly IRepository<Training, TrainingCreation> _trainingsData;
        private readonly IRepository<Person, PersonCreation> _personsData;
        private readonly IRepository<MachineResponsible, MachineResponsibleCreation> _responsibilitiesData;
        public ActionTransferViewModel(
            ViewModelBaseDependecies dependecies,
            ActivityContext activityContext,
            IRepository<MachineResponsible, MachineResponsibleCreation> responsibilitiesData,
            IRepository<Training, TrainingCreation> trainingsData,
            IRepository<Person, PersonCreation> personsData)
        : base(dependecies)
        {
            if (activityContext == null)
            {
                ReturnCommand.Execute(null);
            }
            Activity = activityContext!.Activity!;
            _trainingsData = trainingsData;
            _responsibilitiesData = responsibilitiesData;
            _personsData = personsData;

            PeopleToTrain = new ListCollectionView(_responsibleTrainings);

            Task.Run(LoadDataAsync);
        }
        public ICollectionView PeopleToTrain { get; }
        public ICommand ReturnCommand => new NavigationCommand<ActionsListViewModel>(NavigationService).Navigate();
        public Activity Activity { get; }
        private async Task LoadDataAsync()
        {
            List<Task> tasks = [];

            IEnumerable<Training> dataOne = (await _trainingsData.GetDataAsync()).Where(x => x.ActivityId == Activity.Id);
            IEnumerable<MachineResponsible> dataTwo = (await _responsibilitiesData.GetDataAsync()).Where(x => x.MachineId == Activity.MachineId);
            List<Person> dataThree = await _personsData.GetDataAsync();

            foreach (MachineResponsible item in dataTwo)
            {
                Person? person = dataThree.Find(x => x.Id == item.PersonId);
                TrainingStatus status = TrainingStatus.Trained;
                if (person == null)
                {
                    break;
                }

                IEnumerable<Training> personTrainings = dataOne.Where(x => x.TraineeId == person.Id);
                if (!personTrainings.Any())
                {
                    status = TrainingStatus.Untrained;
                }

                Training? training = personTrainings.FirstOrDefault(x => x.IsConfirmedByTrainee);
                if (training == null)
                {
                    status = TrainingStatus.InTraining;
                }

                _responsibleTrainings.Add(new ResponsibleTraining()
                {
                    Responsible = person,
                    TrainingStatus = status,
                });
            }

            await Task.WhenAll(tasks);
            PeopleToTrain.Refresh();
        }
    }

    internal class ResponsibleTraining
    {
        required public Person Responsible { get; set; }
        public TrainingStatus TrainingStatus { get; set; } = TrainingStatus.Untrained;
    }
    internal enum TrainingStatus
    {
        Untrained,
        InTraining,
        Trained,
    }
}

// X - nieprzeszkolony
// O - w trakcie szkolenia
// V - przeszkolony