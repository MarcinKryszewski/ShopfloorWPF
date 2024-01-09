using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Services.Providers;

namespace Shopfloor.Models
{
    public class Role
    {
        public int Id { get; }
        public int Value { get; }
        public string Name { get; }

        public Role(int id, string name, int value)
        {
            Id = id;
            Name = name;
            Value = value;
        }

        public Role(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public async Task Add(RoleProvider provider)
        {
            await provider.Create(this);
        }
        public async Task Edit(RoleProvider provider)
        {
            await provider.Update(this);
        }
        public async Task Delete(RoleProvider provider)
        {
            await provider.Delete(Id);
        }
    }
}