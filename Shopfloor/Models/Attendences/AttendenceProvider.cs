using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Attendences
{
    internal class AttendenceProvider : IProvider<Attendence, AttendenceCreation>
    {
        public Task<int> Create(AttendenceCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Attendence>> GetAll()
        {
            return Task.FromResult(TestData());
        }
        public Task<Attendence?> GetById(int id)
        {
            List<Attendence> data = [.. TestData()];
            Attendence? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(Attendence item)
        {
            return Task.CompletedTask;
        }
        private static IEnumerable<Attendence> TestData()
        {
            IEnumerable<Attendence> data = [
                new Attendence { CourseId = 1, TrainingId = 1, TrainingDate = DateTime.Now },
                new Attendence { CourseId = 2, TrainingId = 2, TrainingDate = DateTime.Now.AddDays(-1), },
                new Attendence { CourseId = 3, TrainingId = 1, TrainingDate = DateTime.Now.AddDays(-2), },
                new Attendence { CourseId = 4, TrainingId = 4, TrainingDate = DateTime.Now.AddDays(-3), },
            ];

            return data;
        }
    }
}