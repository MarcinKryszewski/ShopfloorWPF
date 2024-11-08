using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.ActivitiesInstructions
{
    internal class ActivityInstructionProvider : IProvider<ActivityInstruction, ActivityInstructionCreation>
    {
        public Task<int> Create(ActivityInstructionCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<ActivityInstruction>> GetAll()
        {
            return Task.FromResult(TestData());
        }
        public Task<ActivityInstruction?> GetById(int id)
        {
            return Task.FromResult(TestData().ToList().Find(x => x.Id == id));
        }
        public Task Update(ActivityInstruction item)
        {
            return Task.CompletedTask;
        }
        private static IEnumerable<ActivityInstruction> TestData()
        {
            IEnumerable<ActivityInstruction> data = [
                new ActivityInstruction { Id = 1, ActivityId = 1, InstructionId = 1 },
                new ActivityInstruction { Id = 2, ActivityId = 1, InstructionId = 2 },
                new ActivityInstruction { Id = 3, ActivityId = 3, InstructionId = 3 },
                new ActivityInstruction { Id = 4, ActivityId = 4, InstructionId = 4 },
                new ActivityInstruction { Id = 5, ActivityId = 5, InstructionId = 5 },
                new ActivityInstruction { Id = 6, ActivityId = 6, InstructionId = 6 },
                new ActivityInstruction { Id = 7, ActivityId = 7, InstructionId = 7 },
                new ActivityInstruction { Id = 8, ActivityId = 8, InstructionId = 8 },
                new ActivityInstruction { Id = 9, ActivityId = 9, InstructionId = 9 },
                new ActivityInstruction { Id = 10, ActivityId = 10, InstructionId = 10 },
                new ActivityInstruction { Id = 11, ActivityId = 11, InstructionId = 11 },
                new ActivityInstruction { Id = 12, ActivityId = 12, InstructionId = 12 },
                new ActivityInstruction { Id = 13, ActivityId = 13, InstructionId = 13 },
                new ActivityInstruction { Id = 14, ActivityId = 14, InstructionId = 14 },
                new ActivityInstruction { Id = 15, ActivityId = 15, InstructionId = 15 },
                new ActivityInstruction { Id = 16, ActivityId = 16, InstructionId = 16 },
                new ActivityInstruction { Id = 17, ActivityId = 17, InstructionId = 17 },
                new ActivityInstruction { Id = 18, ActivityId = 18, InstructionId = 18 },
                new ActivityInstruction { Id = 19, ActivityId = 19, InstructionId = 19 },
                new ActivityInstruction { Id = 20, ActivityId = 20, InstructionId = 20 },
                new ActivityInstruction { Id = 21, ActivityId = 21, InstructionId = 21 },
                new ActivityInstruction { Id = 22, ActivityId = 22, InstructionId = 22 },
                new ActivityInstruction { Id = 23, ActivityId = 23, InstructionId = 23 },
                new ActivityInstruction { Id = 24, ActivityId = 24, InstructionId = 24 },
                new ActivityInstruction { Id = 25, ActivityId = 25, InstructionId = 25 },
                new ActivityInstruction { Id = 26, ActivityId = 26, InstructionId = 26 },
                new ActivityInstruction { Id = 27, ActivityId = 27, InstructionId = 27 },
                new ActivityInstruction { Id = 28, ActivityId = 28, InstructionId = 28 },
                new ActivityInstruction { Id = 29, ActivityId = 1, InstructionId = 29 },
                new ActivityInstruction { Id = 30, ActivityId = 3, InstructionId = 30 },
            ];

            return data;
        }
    }
}