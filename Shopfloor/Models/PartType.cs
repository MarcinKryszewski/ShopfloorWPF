using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Services.Providers;

namespace Shopfloor.Models
{
    public class PartType
    {
        public int Id { get; }
        public string Name { get; }

        public PartType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public PartType(string name)
        {
            Name = name;
        }

        public async Task Add(PartTypeProvider provider)
        {
            await provider.Create(this);
        }
        public async Task Edit(PartTypeProvider provider)
        {
            await provider.Update(this);
        }
        public async Task Delete(PartTypeProvider provider)
        {
            await provider.Delete(Id);
        }
    }
}