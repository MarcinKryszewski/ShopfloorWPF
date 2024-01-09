using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Services.Providers;

namespace Shopfloor.Models
{
    public class User
    {
        private readonly HashSet<int> _roles;
        private readonly int _id;
        private readonly string _username;
        private readonly string _name;
        private readonly string _surname;
        private string _image;

        public int Id => _id;
        public string Username => _username;
        public string Image => _image;
        public string Name => _name;
        public string Surname => _surname;
        public string FullName => $"{_name} {_surname}";

        public User(int id, string username, string name, string surname, string imagePath)
        {
            _id = id;
            _username = username;
            _name = name;
            _surname = surname;
            _roles = new();
            _image = imagePath.Length > 0 ? imagePath : "pack://application:,,,/Shopfloor;component/Resources/userDefault.png";
        }

        public User(string username)
        {
            _username = username;
            _name = string.Empty;
            _surname = string.Empty;
            _roles = new();
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
        public void AddRole(Role role)
        {
            _roles.Add(role.Value);
        }
        public bool IsAuthorized(int roleValue)
        {
            return _roles.Contains(roleValue);
        }
    }
}