using System;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Courses;
using Shopfloor.Models.Trainings;

namespace Shopfloor.Models.Attendences
{
    internal class AttendenceCreation : ModelValidationBase, IModelCreationModel<Attendence>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsConfirmedByCoach { get; set; }
        public bool IsConfirmedByTrainee { get; set; }
        public Course? Course { get; set; }
        public int CourseId { get; set; }
        public Training? Training { get; set; }
        public int TrainingId { get; set; }
        public DateTime? TrainingDate { get; set; }
        public Attendence? CreateModel(int id)
        {
            if (Course is null || Training is null)
            {
                return null;
            }

            return new Attendence()
            {
                Id = id,
                Name = Name,
                Course = Course,
                Training = Training,
                CourseId = CourseId,
                TrainingId = TrainingId,
                TrainingDate = TrainingDate,
                IsConfirmedByCoach = IsConfirmedByCoach,
                IsConfirmedByTrainee = IsConfirmedByTrainee,
            };
        }
    }
}