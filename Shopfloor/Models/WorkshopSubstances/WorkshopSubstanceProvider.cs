using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.WorkshopSubstances
{
    internal class WorkshopSubstanceProvider : IProvider<WorkshopSubstance, WorkshopSubstanceCreation>
    {
        public Task<int> Create(WorkshopSubstanceCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<WorkshopSubstance>> GetAll()
        {
            return Task.FromResult(TestData());
        }
        public Task<WorkshopSubstance?> GetById(int id)
        {
            return Task.FromResult(TestData().ToList().Find(x => x.Id == id));
        }
        public Task Update(WorkshopSubstance item)
        {
            return Task.CompletedTask;
        }
        private static IEnumerable<WorkshopSubstance> TestData()
        {
            IEnumerable<WorkshopSubstance> data = [
                new WorkshopSubstance { Id = 1, WorkshopId = 1, SubstanceId = 1 },
                new WorkshopSubstance { Id = 2, WorkshopId = 1, SubstanceId = 2 },
                new WorkshopSubstance { Id = 3, WorkshopId = 3, SubstanceId = 3 },
                new WorkshopSubstance { Id = 4, WorkshopId = 4, SubstanceId = 4 },
                new WorkshopSubstance { Id = 5, WorkshopId = 5, SubstanceId = 5 },
                new WorkshopSubstance { Id = 6, WorkshopId = 6, SubstanceId = 6 },
                new WorkshopSubstance { Id = 7, WorkshopId = 7, SubstanceId = 7 },
                new WorkshopSubstance { Id = 8, WorkshopId = 8, SubstanceId = 8 },
                new WorkshopSubstance { Id = 9, WorkshopId = 9, SubstanceId = 9 },
                new WorkshopSubstance { Id = 10, WorkshopId = 10, SubstanceId = 10 },
                new WorkshopSubstance { Id = 11, WorkshopId = 11, SubstanceId = 11 },
                new WorkshopSubstance { Id = 12, WorkshopId = 12, SubstanceId = 12 },
                new WorkshopSubstance { Id = 13, WorkshopId = 13, SubstanceId = 13 },
                new WorkshopSubstance { Id = 14, WorkshopId = 14, SubstanceId = 14 },
                new WorkshopSubstance { Id = 15, WorkshopId = 15, SubstanceId = 15 },
                new WorkshopSubstance { Id = 16, WorkshopId = 16, SubstanceId = 16 },
                new WorkshopSubstance { Id = 17, WorkshopId = 17, SubstanceId = 17 },
                new WorkshopSubstance { Id = 18, WorkshopId = 18, SubstanceId = 18 },
                new WorkshopSubstance { Id = 19, WorkshopId = 19, SubstanceId = 19 },
                new WorkshopSubstance { Id = 20, WorkshopId = 20, SubstanceId = 20 },
                new WorkshopSubstance { Id = 21, WorkshopId = 21, SubstanceId = 21 },
                new WorkshopSubstance { Id = 22, WorkshopId = 22, SubstanceId = 22 },
                new WorkshopSubstance { Id = 23, WorkshopId = 23, SubstanceId = 23 },
                new WorkshopSubstance { Id = 24, WorkshopId = 24, SubstanceId = 24 },
                new WorkshopSubstance { Id = 25, WorkshopId = 25, SubstanceId = 25 },
                new WorkshopSubstance { Id = 26, WorkshopId = 26, SubstanceId = 26 },
                new WorkshopSubstance { Id = 27, WorkshopId = 27, SubstanceId = 27 },
                new WorkshopSubstance { Id = 28, WorkshopId = 28, SubstanceId = 28 },
                new WorkshopSubstance { Id = 29, WorkshopId = 1, SubstanceId = 29 },
                new WorkshopSubstance { Id = 30, WorkshopId = 3, SubstanceId = 30 },
            ];

            return data;
        }
    }
}