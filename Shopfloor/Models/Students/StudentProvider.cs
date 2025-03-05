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
            IEnumerable<Student> data = [
                new Student { Id = 1, PersonId = 1, TrainingId = 1, },
                new Student { Id = 2, PersonId = 1, TrainingId = 1, },
                new Student { Id = 3, PersonId = 1, TrainingId = 1, },
                new Student { Id = 4, PersonId = 1, TrainingId = 1, },
            ];
            return Task.FromResult(data);
        }
        public Task<Student?> GetById(int id)
        {
            List<Student> data = [
                new Student { Id = 1, PersonId = 1, TrainingId = 1, },
                new Student { Id = 2, PersonId = 1, TrainingId = 1, },
                new Student { Id = 3, PersonId = 1, TrainingId = 1, },
                new Student { Id = 4, PersonId = 1, TrainingId = 1, },
            ];

            Student? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(Student item)
        {
            return Task.CompletedTask;
        }
    }
}