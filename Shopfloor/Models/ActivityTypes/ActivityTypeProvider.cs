using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.ActivityTypes
{
    internal class ActivityTypeProvider : IProvider<ActivityType, ActivityTypeCreation>
    {
        public Task<int> Create(ActivityTypeCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<ActivityType>> GetAll()
        {
            IEnumerable<ActivityType> data = [
                new ActivityType { Id = 1, Name = "CILT", },
                new ActivityType { Id = 2, Name = "Remont", },
            ];
            return Task.FromResult(data);
        }
        public Task<ActivityType?> GetById(int id)
        {
            List<ActivityType> data = [
                new ActivityType { Id = 1, Name = "CILT", },
                new ActivityType { Id = 2, Name = "Remont", },
            ];

            ActivityType? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(ActivityType item)
        {
            return Task.CompletedTask;
        }
    }
}