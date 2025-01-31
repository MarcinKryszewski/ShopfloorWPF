using Shopfloor.Models.Persons;
using Shopfloor.Models.Trainings;
using Shopfloor.Roots;
using Shopfloor.Services.AuthServices;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Action.TrainingsList.Commands
{
    internal class ConfirmTrainingCommand : CommandBase
    {
        private readonly IUserContext _userContext;
        private readonly TrainingsRoot _trainingRoot;
        public ConfirmTrainingCommand(
            IUserContext userContext,
            TrainingsRoot trainingRoot)
        {
            _userContext = userContext;
            _trainingRoot = trainingRoot;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is not Training)
            {
                return;
            }
            Training trainingData = (Training)parameter;
            ConfirmTraining(trainingData);
            SaveTraining(trainingData);
        }
        private void ConfirmTraining(Training training)
        {
            Person? currentUser = _userContext.Person;
            if (currentUser == null)
            {
                return;
            }

            if (currentUser == training.Coach)
            {
                training.IsConfirmedByCoach = true;
            }

            if (currentUser == training.Trainee)
            {
                training.IsConfirmedByTrainee = true;
            }
        }
        private void SaveTraining(Training training)
        {
            _trainingRoot.ConfirmTraining(training.CreateModelCreation()).Wait();
        }
    }
}