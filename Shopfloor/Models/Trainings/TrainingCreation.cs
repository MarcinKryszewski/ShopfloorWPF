using System;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;

namespace Shopfloor.Models.Trainings
{
    internal class TrainingCreation : ModelValidationBase, IModelCreationModel<Training>
    {
        public Activity? Activity { get; set; }
        public int ActivityId { get; set; }
        public Person? Coach { get; set; }
        public int? CoachId { get; set; }
        required public int Id { get; set; }
        public bool IsConfirmedByCoach { get; set; }
        public bool IsConfirmedByTrainee { get; set; }
        public Person? Trainee { get; set; }
        public int TraineeId { get; set; }
        public DateTime? TrainingDate { get; set; }
        public Training CreateModel(int id)
        {
            return new Training()
            {
                Id = id,
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
    }
}