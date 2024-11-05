using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Lines
{
    internal class LineProvider : IProvider<Line, LineCreation>
    {
        public Task<int> Create(LineCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Line>> GetAll()
        {
            IEnumerable<Line> data = [
                new Line { Id = 1, Name = "Linia puszkowa", },
                new Line { Id = 2, Name = "RB1", },
                new Line { Id = 3, Name = "RB2", },
                new Line { Id = 4, Name = "Linia puszkowa KRONES", },
            ];
            return Task.FromResult(data);
        }
        public Task<Line> GetById(int id)
        {
            List<Line> data = [
                new Line { Id = 1, Name = "Linia puszkowa", },
                new Line { Id = 2, Name = "RB1", },
                new Line { Id = 3, Name = "RB2", },
                new Line { Id = 4, Name = "Linia puszkowa KRONES", },
            ];

            Line result = data.First(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(Line item)
        {
            return Task.CompletedTask;
        }
    }
}