using System;
using System.Collections.Generic;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;

namespace Shopfloor.Models.Trainings
{
    internal class TrainingCreation : ModelValidationBase, IModelCreationModel<Training>
    {
        required public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public int TeacherId { get; set; }
        public Person? Teacher { get; set; }
        public ICollection<int> StundetIds { get; set; } = [];
        public ICollection<Person> Students { get; set; } = [];
        public int ActivityId { get; set; }
        public Activity? Activity { get; set; }
        public DateTime TrainingDate { get; set; }
        public Training CreateModel(int id)
        {
            return new Training()
            {
                Id = id,
                Name = Name,
                TeacherId = TeacherId,
                Teacher = Teacher,
                StundetIds = [.. StundetIds],
                Students = [.. Students],
                Activity = Activity,
                ActivityId = ActivityId,
                TrainingDate = TrainingDate,
            };
        }
    }
}