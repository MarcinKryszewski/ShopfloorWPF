using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Attendences
{
    internal class AttendenceProvider : IProvider<Attendence, AttendenceCreation>
    {
        public Task<int> Create(AttendenceCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Attendence>> GetAll()
        {
            IEnumerable<Attendence> data = [
                new Attendence { Id = 1, Name = "Attendence1", },
                new Attendence { Id = 2, Name = "Attendence2", },
                new Attendence { Id = 3, Name = "Attendence3", },
                new Attendence { Id = 4, Name = "Attendence4", },
            ];
            return Task.FromResult(data);
        }
        public Task<Attendence?> GetById(int id)
        {
            List<Attendence> data = [
                new Attendence { Id = 1, Name = "Attendence1", },
                new Attendence { Id = 2, Name = "Attendence2", },
                new Attendence { Id = 3, Name = "Attendence3", },
                new Attendence { Id = 4, Name = "Attendence4", },
            ];

            Attendence? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(Attendence item)
        {
            return Task.CompletedTask;
        }
    }
}