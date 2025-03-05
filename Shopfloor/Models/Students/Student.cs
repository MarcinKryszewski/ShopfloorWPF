using System;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Trainings;

namespace Shopfloor.Models.Students
{
    internal class Student : IModel
    {
        required public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        required public int PersonId { get; init; }
        public Person? Person { get; set; }
        required public int TrainingId { get; init; }
        public Training? Training { get; set; }
        public bool IsConfirmedByStudent { get; set; } = false;
        public bool IsConfirmedByTeacher { get; set; } = false;
        public void SetValues<T>(IModelCreationModel<T> data)
        where T : IModel
        {
            if (data is not StudentCreation)
            {
                return;
            }

            StudentCreation creation = (StudentCreation)data;

            Name = creation.Name;
            Person = creation.Person;
            Training = creation.Training;
            IsConfirmedByStudent = creation.IsConfirmedByStudent;
            IsConfirmedByTeacher = creation.IsConfirmedByTeacher;
        }
    }
}