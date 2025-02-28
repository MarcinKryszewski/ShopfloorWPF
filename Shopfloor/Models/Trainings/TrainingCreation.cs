using System.Collections.Generic;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Attendences;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;

namespace Shopfloor.Models.Trainings
{
    internal class TrainingCreation : ModelValidationBase, IModelCreationModel<Training>
    {
        public Activity? Activity { get; set; }
        public int ActivityId { get; set; }
        required public int Id { get; set; }
        public Person? Trainee { get; set; }
        public int TraineeId { get; set; }
        public List<Attendence> Attendences { get; init; } = [];
        public Training CreateModel(int id)
        {
            return new Training()
            {
                Id = id,
                Activity = Activity,
                ActivityId = ActivityId,
                Trainee = Trainee,
                TraineeId = TraineeId,
                Attendences = Attendences,
            };
        }
    }
}