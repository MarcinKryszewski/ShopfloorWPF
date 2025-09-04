using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Substances
{
    internal class SubstanceProvider : IProvider<Substance, SubstanceCreation>
    {
        public Task<int> Create(SubstanceCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Substance>> GetAll()
        {
            IEnumerable<Substance> data = [
                new Substance { Id = 1, Name = "Loctite 638", ProducerName = "Henkel", IsActive = true, },
                new Substance { Id = 2, Name = "Ferisol", ProducerName = "Ecolab", IsActive = true, },
                new Substance { Id = 3, Name = "Celerol L7102", ProducerName = "Krones", IsActive = true, },
                new Substance { Id = 4, Name = "UH1 6-460", ProducerName = "KLUEBERSYNTH", IsActive = true, },
            ];
            return Task.FromResult(data);
        }
        public Task<Substance?> GetById(int id)
        {
            List<Substance> data = [
                new Substance { Id = 1, Name = "Loctite 638", ProducerName = "Henkel", IsActive = true, },
                new Substance { Id = 2, Name = "Ferisol", ProducerName = "Ecolab", IsActive = true, },
                new Substance { Id = 3, Name = "Celerol L7102", ProducerName = "Krones", IsActive = true, },
                new Substance { Id = 4, Name = "UH1 6-460", ProducerName = "KLUEBERSYNTH", IsActive = true, },
            ];

            Substance? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(Substance item)
        {
            return Task.CompletedTask;
        }
    }
}