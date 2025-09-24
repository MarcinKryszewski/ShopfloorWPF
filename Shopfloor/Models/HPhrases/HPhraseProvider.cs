using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.HPhrases
{
    internal class HPhraseProvider : IProvider<HPhrase, HPhraseCreation>
    {
        public Task<int> Create(HPhraseCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<HPhrase>> GetAll()
        {
            IEnumerable<HPhrase> data = [
                new HPhrase { Id = 1, Code = "TEST1", },
                new HPhrase { Id = 2, Code = "TEST2", },
            ];
            return Task.FromResult(data);
        }
        public Task<HPhrase?> GetById(int id)
        {
            List<HPhrase> data = [
                new HPhrase { Id = 1, Code = "TEST1", },
                new HPhrase { Id = 2, Code = "TEST2", },
            ];

            HPhrase? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(HPhrase item)
        {
            return Task.CompletedTask;
        }
    }
}