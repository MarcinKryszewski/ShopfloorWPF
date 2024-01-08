using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Services.Providers;

namespace Shopfloor.Models
{
    public class Permission
    {
        public int Id { get; }
        public int Value { get; }

        public Permission(int id, int value)
        {
            Id = id;
            Value = value;
        }

        public Permission(int value)
        {
            Value = value;
        }

        public async Task Add(PermissionProvider provider)
        {
            await provider.Create(this);
        }
        public async Task Edit(PermissionProvider provider)
        {
            await provider.Update(this);
        }
        public async Task Delete(PermissionProvider provider)
        {
            await provider.Delete(Id);
        }
    }
}