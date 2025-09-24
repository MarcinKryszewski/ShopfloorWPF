using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.SubstanceHazards
{
    internal class SubstanceHazardProvider : IProvider<SubstanceHazard, SubstanceHazardCreation>
    {
        public Task<int> Create(SubstanceHazardCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<SubstanceHazard>> GetAll()
        {
            IEnumerable<SubstanceHazard> data = [
                new SubstanceHazard { Id = 1, Name = "TEST1", },
                new SubstanceHazard { Id = 2, Name = "TEST2", },
            ];
            return Task.FromResult(data);
        }
        public Task<SubstanceHazard?> GetById(int id)
        {
            List<SubstanceHazard> data = [
                new SubstanceHazard { Id = 1, Name = "TEST1", },
                new SubstanceHazard { Id = 2, Name = "TEST2", },
            ];

            SubstanceHazard? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(SubstanceHazard item)
        {
            return Task.CompletedTask;
        }
    }
}