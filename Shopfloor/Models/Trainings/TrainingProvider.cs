using System;
using System.Collections.Generic;
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
            IEnumerable<Training> data = [
                new Training { Id = 1, },
                new Training { Id = 2, },
                new Training { Id = 3, },
                new Training { Id = 4, },
            ];
            return Task.FromResult(data);
        }
        public Task<Training?> GetById(int id)
        {
            List<Training> data = [
                new Training { Id = 1, },
                new Training { Id = 2, },
                new Training { Id = 3, },
                new Training { Id = 4, },
            ];

            Training? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(Training item)
        {
            return Task.CompletedTask;
        }
    }
}