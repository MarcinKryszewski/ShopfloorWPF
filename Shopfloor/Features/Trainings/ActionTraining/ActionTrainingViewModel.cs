using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Shopfloor.Features.Actions.ActionTransfer.Utilities;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.MachinesResponsibles;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Trainings;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Utilities.Collections;

namespace Shopfloor.Features.Trainings.ActionTraining
{
    internal class ActionTrainingViewModel : ViewModelBase
    {
        public ActionTrainingViewModel(IRepository<Training, TrainingCreation> trainingRepository, IRepository<Person, PersonCreation> personRepository)
        {
            FillTrainingList(trainingRepository.GetDataAsync().Result, personRepository.GetDataAsync().Result);
        }
        public ICommand TrainCommand { get; }
        public ICommand RetrainCommand { get; }
        public ICommand CancelTrainingCommand { get; }
        public ICommand ReturnCommand { get; }
        public ConcurrentObservableCollection<ResponsibleTraining> TrainingList { get; private set; } = [];
        private void FillTrainingList(IEnumerable<Training> trainings, List<Person> persons)
        {
            TrainingList.Clear();

            foreach (Person person in persons)
            {
                IEnumerable<Training> personTrainings = trainings.Where(x => x.StundetIds.Contains(person.Id));

                TrainingList.Add(new ResponsibleTraining()
                {
                    Responsible = person,
                    TrainingStatus = TrainingStatusRetriever.GetTrainingStatus(personTrainings),
                });
            }
        }
    }
}