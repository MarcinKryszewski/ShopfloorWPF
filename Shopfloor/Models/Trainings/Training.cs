using System;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;

namespace Shopfloor.Models.Trainings
{
    internal class Training : IModel
    {
        public Activity? Activity { get; set; }
        public int ActivityId { get; init; }
        public Person? Coach { get; set; }
        public int CoachId { get; init; }
        required public int Id { get; init; }
        public bool IsConfirmedByCoach { get; set; }
        public bool IsConfirmedByTrainee { get; set; }
        public string Name { get; } = string.Empty;
        public Person? Trainee { get; set; }
        public int TraineeId { get; init; }
        public DateTime? TrainingDate { get; set; }
        public TrainingCreation CreateModelCreation()
        {
            return new TrainingCreation()
            {
                Id = Id,
                Activity = Activity,
                ActivityId = ActivityId,
                Coach = Coach,
                CoachId = CoachId,
                IsConfirmedByCoach = IsConfirmedByCoach,
                IsConfirmedByTrainee = IsConfirmedByTrainee,
                Trainee = Trainee,
                TraineeId = TraineeId,
                TrainingDate = TrainingDate,
            };
        }
        public void SetValues<T>(IModelCreationModel<T> data)
                    where T : IModel
        {
            if (data is not TrainingCreation)
            {
                return;
            }

            TrainingCreation creation = (TrainingCreation)data;

            Activity = creation.Activity;
            Coach = creation.Coach;
            IsConfirmedByCoach = creation.IsConfirmedByCoach;
            IsConfirmedByTrainee = creation.IsConfirmedByTrainee;
            Trainee = creation.Trainee;
            TrainingDate = creation.TrainingDate;
        }
    }
}