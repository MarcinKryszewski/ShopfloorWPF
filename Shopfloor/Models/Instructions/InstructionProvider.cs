using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Instructions
{
    internal class InstructionProvider : IProvider<Instruction, InstructionCreation>
    {
        public Task<int> Create(InstructionCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Instruction>> GetAll()
        {
            IEnumerable<Instruction> data = TestData();

            return Task.FromResult(data);
        }
        public Task<Instruction?> GetById(int id)
        {
            List<Instruction> data = TestData().ToList();
            return Task.FromResult(data.Find(x => x.Id == id));
        }
        public Task Update(Instruction item)
        {
            return Task.CompletedTask;
        }
        private static IEnumerable<Instruction> TestData()
        {
            IEnumerable<Instruction> data = [
                new Instruction { Id = 1, AcceptedById = 1, AuthorId = 1, CreationDate = DateTime.Now.AddDays(-3), FilePath = "path/to/file1", IsAccepted = true, Version = 1 },
                new Instruction { Id = 2, AcceptedById = 2, AuthorId = 2, CreationDate = DateTime.Now.AddDays(-10), FilePath = "path/to/file2", IsAccepted = false, Version = 2 },
                new Instruction { Id = 3, AcceptedById = 3, AuthorId = 3, CreationDate = DateTime.Now.AddDays(-20), FilePath = "path/to/file3", IsAccepted = true, Version = 1 },
                new Instruction { Id = 4, AcceptedById = 4, AuthorId = 4, CreationDate = DateTime.Now.AddDays(-30), FilePath = "path/to/file4", IsAccepted = false, Version = 3 },
                new Instruction { Id = 5, AcceptedById = 5, AuthorId = 5, CreationDate = DateTime.Now.AddDays(-40), FilePath = "path/to/file5", IsAccepted = true, Version = 2 },
                new Instruction { Id = 6, AcceptedById = 6, AuthorId = 6, CreationDate = DateTime.Now.AddDays(-50), FilePath = "path/to/file6", IsAccepted = false, Version = 1 },
                new Instruction { Id = 7, AcceptedById = 7, AuthorId = 7, CreationDate = DateTime.Now.AddDays(-60), FilePath = "path/to/file7", IsAccepted = true, Version = 4 },
                new Instruction { Id = 8, AcceptedById = 8, AuthorId = 8, CreationDate = DateTime.Now.AddDays(-70), FilePath = "path/to/file8", IsAccepted = false, Version = 2 },
                new Instruction { Id = 9, AcceptedById = 9, AuthorId = 9, CreationDate = DateTime.Now.AddDays(-80), FilePath = "path/to/file9", IsAccepted = true, Version = 1 },
                new Instruction { Id = 10, AcceptedById = 10, AuthorId = 10, CreationDate = DateTime.Now.AddDays(-90), FilePath = "path/to/file10", IsAccepted = false, Version = 3 },
                new Instruction { Id = 11, AcceptedById = 1, AuthorId = 2, CreationDate = DateTime.Now.AddDays(-100), FilePath = "path/to/file11", IsAccepted = true, Version = 1 },
                new Instruction { Id = 12, AcceptedById = 2, AuthorId = 3, CreationDate = DateTime.Now.AddDays(-110), FilePath = "path/to/file12", IsAccepted = false, Version = 2 },
                new Instruction { Id = 13, AcceptedById = 3, AuthorId = 4, CreationDate = DateTime.Now.AddDays(-120), FilePath = "path/to/file13", IsAccepted = true, Version = 3 },
                new Instruction { Id = 14, AcceptedById = 4, AuthorId = 5, CreationDate = DateTime.Now.AddDays(-130), FilePath = "path/to/file14", IsAccepted = false, Version = 1 },
                new Instruction { Id = 15, AcceptedById = 5, AuthorId = 6, CreationDate = DateTime.Now.AddDays(-140), FilePath = "path/to/file15", IsAccepted = true, Version = 2 },
                new Instruction { Id = 16, AcceptedById = 6, AuthorId = 7, CreationDate = DateTime.Now.AddDays(-150), FilePath = "path/to/file16", IsAccepted = false, Version = 1 },
                new Instruction { Id = 17, AcceptedById = 7, AuthorId = 8, CreationDate = DateTime.Now.AddDays(-160), FilePath = "path/to/file17", IsAccepted = true, Version = 3 },
                new Instruction { Id = 18, AcceptedById = 8, AuthorId = 9, CreationDate = DateTime.Now.AddDays(-170), FilePath = "path/to/file18", IsAccepted = false, Version = 2 },
                new Instruction { Id = 19, AcceptedById = 9, AuthorId = 10, CreationDate = DateTime.Now.AddDays(-180), FilePath = "path/to/file19", IsAccepted = true, Version = 1 },
                new Instruction { Id = 20, AcceptedById = 10, AuthorId = 1, CreationDate = DateTime.Now.AddDays(-190), FilePath = "path/to/file20", IsAccepted = false, Version = 4 },
                new Instruction { Id = 21, AcceptedById = 1, AuthorId = 3, CreationDate = DateTime.Now.AddDays(-200), FilePath = "path/to/file21", IsAccepted = true, Version = 2 },
                new Instruction { Id = 22, AcceptedById = 2, AuthorId = 4, CreationDate = DateTime.Now.AddDays(-210), FilePath = "path/to/file22", IsAccepted = false, Version = 1 },
                new Instruction { Id = 23, AcceptedById = 3, AuthorId = 5, CreationDate = DateTime.Now.AddDays(-220), FilePath = "path/to/file23", IsAccepted = true, Version = 3 },
                new Instruction { Id = 24, AcceptedById = 4, AuthorId = 6, CreationDate = DateTime.Now.AddDays(-230), FilePath = "path/to/file24", IsAccepted = false, Version = 2 },
                new Instruction { Id = 25, AcceptedById = 5, AuthorId = 7, CreationDate = DateTime.Now.AddDays(-240), FilePath = "path/to/file25", IsAccepted = true, Version = 1 },
                new Instruction { Id = 26, AcceptedById = 6, AuthorId = 8, CreationDate = DateTime.Now.AddDays(-250), FilePath = "path/to/file26", IsAccepted = false, Version = 4 },
                new Instruction { Id = 27, AcceptedById = 7, AuthorId = 9, CreationDate = DateTime.Now.AddDays(-260), FilePath = "path/to/file27", IsAccepted = true, Version = 2 },
                new Instruction { Id = 28, AcceptedById = 8, AuthorId = 10, CreationDate = DateTime.Now.AddDays(-270), FilePath = "path/to/file28", IsAccepted = false, Version = 1 },
                new Instruction { Id = 29, AcceptedById = 9, AuthorId = 1, CreationDate = DateTime.Now.AddDays(-280), FilePath = "path/to/file29", IsAccepted = true, Version = 3 },
                new Instruction { Id = 30, AcceptedById = 10, AuthorId = 2, CreationDate = DateTime.Now.AddDays(-290), FilePath = "path/to/file30", IsAccepted = false, Version = 2 },
            ];

            return data;
        }
    }
}