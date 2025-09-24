using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.HazardousSubstances
{
    internal class HazardousSubstanceProvider : IProvider<HazardousSubstance, HazardousSubstanceCreation>
    {
        public Task<int> Create(HazardousSubstanceCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<HazardousSubstance>> GetAll()
        {
            IEnumerable<HazardousSubstance> data = [
                new HazardousSubstance { Id = 1, Name = "TEST1", },
                new HazardousSubstance { Id = 2, Name = "TEST2", },
            ];
            return Task.FromResult(data);
        }
        public Task<HazardousSubstance?> GetById(int id)
        {
            List<HazardousSubstance> data = [
                new HazardousSubstance { Id = 1, Name = "TEST1", },
                new HazardousSubstance { Id = 2, Name = "TEST2", },
            ];

            HazardousSubstance? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(HazardousSubstance item)
        {
            return Task.CompletedTask;
        }
    }
}