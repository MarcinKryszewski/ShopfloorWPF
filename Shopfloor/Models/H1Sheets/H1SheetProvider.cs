using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.H1Sheets
{
    internal class H1SheetProvider : IProvider<H1Sheet, H1SheetCreation>
    {
        public Task<int> Create(H1SheetCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<H1Sheet>> GetAll()
        {
            IEnumerable<H1Sheet> data = [
                new H1Sheet { Id = 1, Name = "TEST1", },
                new H1Sheet { Id = 2, Name = "TEST2", },
            ];
            return Task.FromResult(data);
        }
        public Task<H1Sheet?> GetById(int id)
        {
            List<H1Sheet> data = [
                new H1Sheet { Id = 1, Name = "TEST1", },
                new H1Sheet { Id = 2, Name = "TEST2", },
            ];

            H1Sheet? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(H1Sheet item)
        {
            return Task.CompletedTask;
        }
    }
}