using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Students
{
    internal class StudentProvider : IProvider<Student, StudentCreation>
    {
        public Task<int> Create(StudentCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Student>> GetAll()
        {

            return Task.FromResult(TestData());
        }
        public Task<Student?> GetById(int id)
        {
            return Task.FromResult(TestData().ToList().Find(x => x.Id == id));
        }
        public Task Update(Student item)
        {
            return Task.CompletedTask;
        }
        private static IEnumerable<Student> TestData()
        {
            IEnumerable<Student> data = [
                new Student { Id = 1, PersonId = 3, TrainingId = 1, IsConfirmedByStudent = true, IsConfirmedByTeacher = false},
                new Student { Id = 2, PersonId = 7, TrainingId = 4, IsConfirmedByStudent = false, IsConfirmedByTeacher = true},
                new Student { Id = 3, PersonId = 1, TrainingId = 2, IsConfirmedByStudent = true, IsConfirmedByTeacher = true},
                new Student { Id = 4, PersonId = 9, TrainingId = 6, IsConfirmedByStudent = false, IsConfirmedByTeacher = false},
                new Student { Id = 5, PersonId = 5, TrainingId = 3, IsConfirmedByStudent = true, IsConfirmedByTeacher = false},
                new Student { Id = 6, PersonId = 2, TrainingId = 5, IsConfirmedByStudent = false, IsConfirmedByTeacher = true},
                new Student { Id = 7, PersonId = 8, TrainingId = 1, IsConfirmedByStudent = true, IsConfirmedByTeacher = true},
                new Student { Id = 8, PersonId = 4, TrainingId = 4, IsConfirmedByStudent = false, IsConfirmedByTeacher = false},
                new Student { Id = 9, PersonId = 10, TrainingId = 2, IsConfirmedByStudent = true, IsConfirmedByTeacher = false},
                new Student { Id = 10, PersonId = 6, TrainingId = 6, IsConfirmedByStudent = false, IsConfirmedByTeacher = true},
                new Student { Id = 11, PersonId = 3, TrainingId = 3, IsConfirmedByStudent = true, IsConfirmedByTeacher = true},
                new Student { Id = 12, PersonId = 7, TrainingId = 1, IsConfirmedByStudent = false, IsConfirmedByTeacher = false},
                new Student { Id = 13, PersonId = 1, TrainingId = 5, IsConfirmedByStudent = true, IsConfirmedByTeacher = false},
                new Student { Id = 14, PersonId = 9, TrainingId = 4, IsConfirmedByStudent = false, IsConfirmedByTeacher = true},
                new Student { Id = 15, PersonId = 5, TrainingId = 2, IsConfirmedByStudent = true, IsConfirmedByTeacher = true},
                new Student { Id = 16, PersonId = 2, TrainingId = 6, IsConfirmedByStudent = false, IsConfirmedByTeacher = false},
                new Student { Id = 17, PersonId = 8, TrainingId = 3, IsConfirmedByStudent = true, IsConfirmedByTeacher = false},
                new Student { Id = 18, PersonId = 4, TrainingId = 1, IsConfirmedByStudent = false, IsConfirmedByTeacher = true},
                new Student { Id = 19, PersonId = 10, TrainingId = 5, IsConfirmedByStudent = true, IsConfirmedByTeacher = true},
                new Student { Id = 20, PersonId = 6, TrainingId = 4, IsConfirmedByStudent = false, IsConfirmedByTeacher = false}
            ];

            return data;
        }
    }
}