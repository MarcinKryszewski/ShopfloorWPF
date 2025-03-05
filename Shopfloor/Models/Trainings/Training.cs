using System;
using System.Collections.Generic;
using Shopfloor.Models.Activities;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;

namespace Shopfloor.Models.Trainings
{
    internal class Training : IModel
    {
        required public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public int TeacherId { get; set; }
        public Person? Teacher { get; set; }
        public List<int> StundetIds { get; init; } = [];
        public List<Person> Students { get; init; } = [];
        required public int ActivityId { get; init; }
        public Activity? Activity { get; set; }
        public DateTime TrainingDate { get; set; }
        public void SetValues<T>(IModelCreationModel<T> data)
                    where T : IModel
        {
            if (data is not TrainingCreation)
            {
                return;
            }

            TrainingCreation creation = (TrainingCreation)data;

            Name = creation.Name;
            TeacherId = creation.TeacherId;
            Teacher = creation.Teacher;

            StundetIds.Clear();
            StundetIds.AddRange(creation.StundetIds);

            Students.Clear();
            Students.AddRange(creation.Students);

            TrainingDate = creation.TrainingDate;
        }
    }
}