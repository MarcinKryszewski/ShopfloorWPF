using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.SafetySheets
{
    internal class SafetySheetProvider : IProvider<SafetySheet, SafetySheetCreation>
    {
        public Task<int> Create(SafetySheetCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<SafetySheet>> GetAll()
        {
            IEnumerable<SafetySheet> data = [
                new SafetySheet { Id = 1, Name = "TEST1", },
                new SafetySheet { Id = 2, Name = "TEST2", },
            ];
            return Task.FromResult(data);
        }
        public Task<SafetySheet?> GetById(int id)
        {
            List<SafetySheet> data = [
                new SafetySheet { Id = 1, Name = "TEST1", },
                new SafetySheet { Id = 2, Name = "TEST2", },
            ];

            SafetySheet? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(SafetySheet item)
        {
            return Task.CompletedTask;
        }
    }
}