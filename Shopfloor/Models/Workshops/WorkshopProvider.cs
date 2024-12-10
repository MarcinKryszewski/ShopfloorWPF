using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Workshops
{
    internal class WorkshopProvider : IProvider<Workshop, WorkshopCreation>
    {
        public Task<int> Create(WorkshopCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Workshop>> GetAll()
        {
            IEnumerable<Workshop> data = [
                new Workshop { Id = 1, Name = "Warsztat mechaniczny", },
                new Workshop { Id = 2, Name = "Warsztat elektroniczny", },
                new Workshop { Id = 3, Name = "Operatorzy", },
            ];
            return Task.FromResult(data);
        }
        public Task<Workshop?> GetById(int id)
        {
            List<Workshop> data = [
                new Workshop { Id = 1, Name = "Warsztat mechaniczny", },
                new Workshop { Id = 2, Name = "Warsztat elektroniczny", },
                new Workshop { Id = 3, Name = "Operatorzy", },
            ];

            Workshop? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(Workshop item)
        {
            return Task.CompletedTask;
        }
    }
}