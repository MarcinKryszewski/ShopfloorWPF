using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.HazardPictograms
{
    internal class HazardPictogramProvider : IProvider<HazardPictogram, HazardPictogramCreation>
    {
        public Task<int> Create(HazardPictogramCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<HazardPictogram>> GetAll()
        {
            IEnumerable<HazardPictogram> data = [
                new HazardPictogram { Id = 1, Name = "TEST1", },
                new HazardPictogram { Id = 2, Name = "TEST2", },
            ];
            return Task.FromResult(data);
        }
        public Task<HazardPictogram?> GetById(int id)
        {
            List<HazardPictogram> data = [
                new HazardPictogram { Id = 1, Name = "TEST1", },
                new HazardPictogram { Id = 2, Name = "TEST2", },
            ];

            HazardPictogram? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(HazardPictogram item)
        {
            return Task.CompletedTask;
        }
    }
}