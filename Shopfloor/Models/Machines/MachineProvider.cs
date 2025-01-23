using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Machines
{
    internal class MachineProvider : IProvider<Machine, MachineCreation>
    {
        public Task<int> Create(MachineCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Machine>> GetAll()
        {
            IEnumerable<Machine> data = [
                new Machine { Id = 1, LineId = 4, Name = "Lathe Machine A", ResponsibleIds = [3] },
                new Machine { Id = 2, LineId = 2, Name = "CNC Milling Machine B", ResponsibleIds = [5] },
                new Machine { Id = 3, LineId = 1, Name = "Drill Press C", ResponsibleIds = [5] },
                new Machine { Id = 4, LineId = 1, Name = "Hydraulic Press D", ResponsibleIds = [4] },
                new Machine { Id = 5, LineId = 3, Name = "Welding Machine E", ResponsibleIds = [10] },
                new Machine { Id = 6, LineId = 1, Name = "Grinding Machine F", ResponsibleIds = [9] },
                new Machine { Id = 7, LineId = 2, Name = "Milling Machine G", ResponsibleIds = [3] },
                new Machine { Id = 8, LineId = 1, Name = "Laser Cutter H", ResponsibleIds = [9] },
                new Machine { Id = 9, LineId = 1, Name = "Polishing Machine I", ResponsibleIds = [3] },
                new Machine { Id = 10, LineId = 3, Name = "Assembly Machine J", ResponsibleIds = [2] },
                new Machine { Id = 11, LineId = 3, Name = "Inspection Machine K", ResponsibleIds = [9] },
                new Machine { Id = 12, LineId = 3, Name = "Packaging Machine L", ResponsibleIds = [8] },
                new Machine { Id = 13, LineId = 3, Name = "Forklift M", ResponsibleIds = [4] },
                new Machine { Id = 14, LineId = 4, Name = "Conveyor Belt N", ResponsibleIds = [2] },
                new Machine { Id = 15, LineId = 2, Name = "Injection Molding Machine O", ResponsibleIds = [5] },
                new Machine { Id = 16, LineId = 3, Name = "Furnace P", ResponsibleIds = [1] },
                new Machine { Id = 17, LineId = 4, Name = "Packing Machine Q", ResponsibleIds = [10] },
                new Machine { Id = 18, LineId = 3, Name = "Heat Treatment Machine R", ResponsibleIds = [1] },
                new Machine { Id = 19, LineId = 1, Name = "Stamping Machine S", ResponsibleIds = [2] },
                new Machine { Id = 20, LineId = 3, Name = "Sorting Machine T", ResponsibleIds = [8] },
            ];

            return Task.FromResult(data);
        }
        public Task<Machine?> GetById(int id)
        {
            List<Machine> data = [
                new Machine { Id = 1, LineId = 4, Name = "Lathe Machine A", ResponsibleIds = [3] },
                new Machine { Id = 2, LineId = 2, Name = "CNC Milling Machine B", ResponsibleIds = [5] },
                new Machine { Id = 3, LineId = 1, Name = "Drill Press C", ResponsibleIds = [5] },
                new Machine { Id = 4, LineId = 1, Name = "Hydraulic Press D", ResponsibleIds = [4] },
                new Machine { Id = 5, LineId = 3, Name = "Welding Machine E", ResponsibleIds = [10] },
                new Machine { Id = 6, LineId = 1, Name = "Grinding Machine F", ResponsibleIds = [9] },
                new Machine { Id = 7, LineId = 2, Name = "Milling Machine G", ResponsibleIds = [3] },
                new Machine { Id = 8, LineId = 1, Name = "Laser Cutter H", ResponsibleIds = [9] },
                new Machine { Id = 9, LineId = 1, Name = "Polishing Machine I", ResponsibleIds = [3] },
                new Machine { Id = 10, LineId = 3, Name = "Assembly Machine J", ResponsibleIds = [2] },
                new Machine { Id = 11, LineId = 3, Name = "Inspection Machine K", ResponsibleIds = [9] },
                new Machine { Id = 12, LineId = 3, Name = "Packaging Machine L", ResponsibleIds = [8] },
                new Machine { Id = 13, LineId = 3, Name = "Forklift M", ResponsibleIds = [4] },
                new Machine { Id = 14, LineId = 4, Name = "Conveyor Belt N", ResponsibleIds = [2] },
                new Machine { Id = 15, LineId = 2, Name = "Injection Molding Machine O", ResponsibleIds = [5] },
                new Machine { Id = 16, LineId = 3, Name = "Furnace P", ResponsibleIds = [1] },
                new Machine { Id = 17, LineId = 4, Name = "Packing Machine Q", ResponsibleIds = [10] },
                new Machine { Id = 18, LineId = 3, Name = "Heat Treatment Machine R", ResponsibleIds = [1] },
                new Machine { Id = 19, LineId = 1, Name = "Stamping Machine S", ResponsibleIds = [2] },
                new Machine { Id = 20, LineId = 3, Name = "Sorting Machine T", ResponsibleIds = [8] },
            ];

            return Task.FromResult(data.Find(x => x.Id == id));
        }
        public Task Update(Machine item)
        {
            return Task.CompletedTask;
        }
    }
}