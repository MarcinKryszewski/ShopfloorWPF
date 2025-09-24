using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.PPhrases
{
    internal class PPhraseProvider : IProvider<PPhrase, PPhraseCreation>
    {
        public Task<int> Create(PPhraseCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<PPhrase>> GetAll()
        {
            IEnumerable<PPhrase> data = [
                new PPhrase { Id = 1, Name = "TEST1", },
                new PPhrase { Id = 2, Name = "TEST2", },
            ];
            return Task.FromResult(data);
        }
        public Task<PPhrase?> GetById(int id)
        {
            List<PPhrase> data = [
                new PPhrase { Id = 1, Name = "TEST1", },
                new PPhrase { Id = 2, Name = "TEST2", },
            ];

            PPhrase? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(PPhrase item)
        {
            return Task.CompletedTask;
        }
    }
}