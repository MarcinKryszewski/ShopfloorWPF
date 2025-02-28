using System;
using System.Collections.Generic;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Courses;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Trainings;

namespace Shopfloor.Models.Attendences
{
    internal class Attendence : IModel
    {
        public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public bool IsConfirmedByCoach { get; set; }
        public bool IsConfirmedByTrainee { get; set; }
        public Course? Course { get; set; }
        required public int CourseId { get; init; }
        public Training? Training { get; set; }
        required public int TrainingId { get; init; }
        public DateTime? TrainingDate { get; set; }
        public AttendenceCreation CreateModelCreation()
        {
            return new AttendenceCreation()
            {
                Id = Id,
                IsConfirmedByCoach = IsConfirmedByCoach,
                IsConfirmedByTrainee = IsConfirmedByTrainee,
                TrainingDate = TrainingDate,
                TrainingId = TrainingId,
                CourseId = CourseId,
                Training = Training,
                Course = Course,
            };
        }
        public void SetValues<T>(IModelCreationModel<T> data)
        where T : IModel
        {
            if (data is not AttendenceCreation)
            {
                return;
            }

            AttendenceCreation creation = (AttendenceCreation)data;

            IsConfirmedByCoach = creation.IsConfirmedByCoach;
            IsConfirmedByTrainee = creation.IsConfirmedByTrainee;
            Name = creation.Name;
            TrainingDate = creation.TrainingDate;
            Course = creation.Course;
            Training = creation.Training;
        }
    }
}