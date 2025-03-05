using System;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Trainings;

namespace Shopfloor.Models.Students
{
    internal class StudentCreation : ModelValidationBase, IModelCreationModel<Student>
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PersonId { get; set; }
        public Person? Person { get; set; }
        public int TrainingId { get; set; }
        public Training? Training { get; set; }
        public bool IsConfirmedByStudent { get; set; } = false;
        public bool IsConfirmedByTeacher { get; set; } = false;
        public Student CreateModel(int id)
        {
            return new Student()
            {
                Id = id,
                Name = Name,
                PersonId = PersonId,
                Person = Person,
                TrainingId = TrainingId,
                Training = Training,
                IsConfirmedByStudent = IsConfirmedByStudent,
                IsConfirmedByTeacher = IsConfirmedByTeacher,
            };
        }
    }
}