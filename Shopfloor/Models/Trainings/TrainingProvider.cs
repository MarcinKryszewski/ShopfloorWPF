using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Trainings
{
    internal class TrainingProvider : IProvider<Training, TrainingCreation>
    {
        public Task<int> Create(TrainingCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Training>> GetAll()
        {
            return Task.FromResult(TestData());
        }
        public Task<Training?> GetById(int id)
        {
            return Task.FromResult(TestData().ToList().Find(x => x.Id == id));
        }
        public Task Update(Training item)
        {
            return Task.CompletedTask;
        }
        private static IEnumerable<Training> TestData()
        {
            IEnumerable<Training> data = [
                new Training { Id = 1, TeacherId = 3, ActivityId = 15, TrainingDate = new DateTime(2023, 11, 15, 10, 0, 0, DateTimeKind.Utc) },
                new Training { Id = 2, TeacherId = 7, ActivityId = 22, TrainingDate = new DateTime(2023, 11, 16, 14, 30, 0, DateTimeKind.Utc) },
                new Training { Id = 3, TeacherId = 1, ActivityId = 5, TrainingDate = new DateTime(2023, 11, 17, 9, 0, 0, DateTimeKind.Utc) },
                new Training { Id = 4, TeacherId = 9, ActivityId = 28, TrainingDate = new DateTime(2023, 11, 18, 16, 15, 0, DateTimeKind.Utc) },
                new Training { Id = 5, TeacherId = 5, ActivityId = 10, TrainingDate = new DateTime(2023, 11, 19, 11, 45, 0, DateTimeKind.Utc) },
                new Training { Id = 6, TeacherId = 2, ActivityId = 3, TrainingDate = new DateTime(2023, 11, 20, 13, 0, 0, DateTimeKind.Utc) }
            ];

            return data;
        }
    }
}