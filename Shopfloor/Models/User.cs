using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Services.Providers;

namespace Shopfloor.Models
{
    public class User
    {
        private readonly HashSet<int> _permissions;

        public int Id { get; }
        public string Username { get; }

        public User(int id, string name)
        {
            Id = id;
            Username = name;
            _permissions = new();
        }

        public User(string name)
        {
            Username = name;
            _permissions = new();
        }

        public async Task Add(UserProvider provider)
        {
            await provider.Create(this);
        }
        public async Task Edit(UserProvider provider)
        {
            await provider.Update(this);
        }
        public async Task Delete(UserProvider provider)
        {
            await provider.Delete(Id);
        }
        public void AddPermission(Permission permission)
        {
            _permissions.Add(permission.Value);
        }
        public bool IsAuthorized(int permissionValue)
        {
            return _permissions.Contains(permissionValue);
        }
    }
}