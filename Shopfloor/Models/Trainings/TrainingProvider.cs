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
            IEnumerable<Training> data = TestData();

            return Task.FromResult(data);
        }
        public Task<Training?> GetById(int id)
        {
            List<Training> data = TestData().ToList();
            return Task.FromResult(data.Find(x => x.Id == id));
        }
        public Task Update(Training item)
        {
            return Task.CompletedTask;
        }
        private static IEnumerable<Training> TestData()
        {
            IEnumerable<Training> data = [
                new Training { Id = 1, ActivityId = 1, TrainingDate = DateTime.Now },
                new Training { Id = 2, ActivityId = 2, TrainingDate = DateTime.Now.AddDays(1) },
                new Training { Id = 3, ActivityId = 3, TrainingDate = DateTime.Now.AddDays(2) },
                new Training { Id = 4, ActivityId = 4, TrainingDate = DateTime.Now.AddDays(3) },
                new Training { Id = 5, ActivityId = 5, TrainingDate = DateTime.Now.AddDays(4) },
                new Training { Id = 6, ActivityId = 6, TrainingDate = DateTime.Now.AddDays(5) },
                new Training { Id = 7, ActivityId = 7, TrainingDate = DateTime.Now.AddDays(6) },
                new Training { Id = 8, ActivityId = 8, TrainingDate = DateTime.Now.AddDays(7) },
                new Training { Id = 9, ActivityId = 9, TrainingDate = DateTime.Now.AddDays(8) },
                new Training { Id = 10, ActivityId = 10, TrainingDate = DateTime.Now.AddDays(9) },
                new Training { Id = 11, ActivityId = 11, TrainingDate = DateTime.Now.AddDays(10) },
                new Training { Id = 12, ActivityId = 12, TrainingDate = DateTime.Now.AddDays(11) },
                new Training { Id = 13, ActivityId = 13, TrainingDate = DateTime.Now.AddDays(12) },
                new Training { Id = 14, ActivityId = 14, TrainingDate = DateTime.Now.AddDays(13) },
                new Training { Id = 15, ActivityId = 15, TrainingDate = DateTime.Now.AddDays(14) },
                new Training { Id = 16, ActivityId = 16, TrainingDate = DateTime.Now.AddDays(15) },
                new Training { Id = 17, ActivityId = 17, TrainingDate = DateTime.Now.AddDays(16) },
                new Training { Id = 18, ActivityId = 18, TrainingDate = DateTime.Now.AddDays(17) },
                new Training { Id = 19, ActivityId = 19, TrainingDate = DateTime.Now.AddDays(18) },
                new Training { Id = 20, ActivityId = 20, TrainingDate = DateTime.Now.AddDays(19) },
                new Training { Id = 21, ActivityId = 21, TrainingDate = DateTime.Now.AddDays(20) },
                new Training { Id = 22, ActivityId = 22, TrainingDate = DateTime.Now.AddDays(21) },
                new Training { Id = 23, ActivityId = 23, TrainingDate = DateTime.Now.AddDays(22) },
                new Training { Id = 24, ActivityId = 24, TrainingDate = DateTime.Now.AddDays(23) },
                new Training { Id = 25, ActivityId = 25, TrainingDate = DateTime.Now.AddDays(24) },
                new Training { Id = 26, ActivityId = 26, TrainingDate = DateTime.Now.AddDays(25) },
                new Training { Id = 27, ActivityId = 27, TrainingDate = DateTime.Now.AddDays(26) },
                new Training { Id = 28, ActivityId = 28, TrainingDate = DateTime.Now.AddDays(27) },
                new Training { Id = 29, ActivityId = 1, TrainingDate = DateTime.Now.AddDays(28) },
                new Training { Id = 30, ActivityId = 2, TrainingDate = DateTime.Now.AddDays(29) },
                new Training { Id = 31, ActivityId = 23, TrainingDate = DateTime.Now.AddDays(29) },
            ];

            return data;
        }
    }
}