using System.Collections.Generic;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Attendences;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;

namespace Shopfloor.Models.Trainings
{
    internal class Training : IModel
    {
        public Activity? Activity { get; set; }
        public int ActivityId { get; init; }
        required public int Id { get; init; }
        public string Name { get; } = string.Empty;
        public Person? Trainee { get; set; }
        public int TraineeId { get; init; }
        public List<Attendence> Attendences { get; init; } = [];
        public TrainingCreation CreateModelCreation()
        {
            return new TrainingCreation()
            {
                Id = Id,
                Activity = Activity,
                ActivityId = ActivityId,
                Trainee = Trainee,
                TraineeId = TraineeId,
                Attendences = Attendences,
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
            Trainee = creation.Trainee;
            Attendences.Clear();
            Attendences.AddRange(creation.Attendences);
        }
    }
}