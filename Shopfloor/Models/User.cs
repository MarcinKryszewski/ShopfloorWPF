using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Services.Providers;

namespace Shopfloor.Models
{
    public class User
    {
        private readonly HashSet<int> _permissions;
        private readonly int _id;
        private readonly string _username;
        private string _image;

        public int Id => _id;
        public string Username => _username;
        public string Image => _image;

        public User(int id, string name)
        {
            _id = id;
            _username = name;
            _permissions = new();
            _image = "pack://application:,,,/Shopfloor;component/Resources/userDefault.png";
        }

        public User(string name)
        {
            _username = name;
            _permissions = new();
            _image = "pack://application:,,,/Shopfloor;component/Resources/userDefault.png";
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
        public void SetImage(string path)
        {
            _image = path;
        }
    }
}