using System.Collections.Generic;
using System.Linq;
using Shopfloor.Models.Trainings;

namespace Shopfloor.Features.Actions.ActionTransfer.Utilities
{
    internal static class TrainingStatusRetriever
    {
        public static TrainingStatus GetTrainingStatus(IEnumerable<Training> personTrainings)
        {
            if (!personTrainings.Any())
            {
                return TrainingStatus.Untrained;
            }

            // Training? training = personTrainings.FirstOrDefault(x => x.IsConfirmedByTrainee);
            // if (training == null)
            // {
            //     return TrainingStatus.InTraining;
            // }

            return TrainingStatus.Trained;
        }
    }
}