using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.MachinesResponsibles
{
    internal class MachineResponsibleProvider : IProvider<MachineResponsible, MachineResponsibleCreation>
    {
        public Task<int> Create(MachineResponsibleCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<MachineResponsible>> GetAll()
        {
            IEnumerable<MachineResponsible> data = [
                new MachineResponsible() { Id = 1, MachineId = 12, PersonId = 4 },
                new MachineResponsible() { Id = 2, MachineId = 20, PersonId = 8 },
                new MachineResponsible() { Id = 3, MachineId = 3, PersonId = 4 },
                new MachineResponsible() { Id = 4, MachineId = 12, PersonId = 10 },
                new MachineResponsible() { Id = 5, MachineId = 5, PersonId = 10 },
                new MachineResponsible() { Id = 6, MachineId = 19, PersonId = 3 },
                new MachineResponsible() { Id = 7, MachineId = 19, PersonId = 9 },
                new MachineResponsible() { Id = 8, MachineId = 9, PersonId = 8 },
                new MachineResponsible() { Id = 9, MachineId = 8, PersonId = 6 },
                new MachineResponsible() { Id = 10, MachineId = 16, PersonId = 10 },
                new MachineResponsible() { Id = 11, MachineId = 18, PersonId = 7 },
                new MachineResponsible() { Id = 12, MachineId = 12, PersonId = 6 },
                new MachineResponsible() { Id = 13, MachineId = 3, PersonId = 3 },
                new MachineResponsible() { Id = 14, MachineId = 5, PersonId = 6 },
                new MachineResponsible() { Id = 15, MachineId = 11, PersonId = 1 },
                new MachineResponsible() { Id = 16, MachineId = 10, PersonId = 5 },
                new MachineResponsible() { Id = 17, MachineId = 2, PersonId = 7 },
                new MachineResponsible() { Id = 18, MachineId = 15, PersonId = 4 },
                new MachineResponsible() { Id = 19, MachineId = 16, PersonId = 3 },
                new MachineResponsible() { Id = 20, MachineId = 15, PersonId = 1 },
                new MachineResponsible() { Id = 21, MachineId = 4, PersonId = 1 },
                new MachineResponsible() { Id = 22, MachineId = 17, PersonId = 1 },
                new MachineResponsible() { Id = 23, MachineId = 17, PersonId = 7 },
                new MachineResponsible() { Id = 24, MachineId = 8, PersonId = 7 },
                new MachineResponsible() { Id = 25, MachineId = 10, PersonId = 1 },
                new MachineResponsible() { Id = 26, MachineId = 11, PersonId = 6 },
                new MachineResponsible() { Id = 27, MachineId = 15, PersonId = 6 },
                new MachineResponsible() { Id = 28, MachineId = 1, PersonId = 10 },
                new MachineResponsible() { Id = 29, MachineId = 16, PersonId = 8 },
                new MachineResponsible() { Id = 30, MachineId = 18, PersonId = 8 },
            ];

            return Task.FromResult(data);
        }
        public Task<MachineResponsible?> GetById(int id)
        {
            throw new NotSupportedException();
        }
        public Task Update(MachineResponsible item)
        {
            throw new NotSupportedException();
        }
    }
}