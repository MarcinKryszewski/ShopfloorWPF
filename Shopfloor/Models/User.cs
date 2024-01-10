using Shopfloor.Services.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models
{
    public class User
    {
        private readonly HashSet<Role> _roles;
        private readonly int _id;
        private readonly string _username;
        private readonly string _name;
        private readonly string _surname;
        private readonly string _image;
        private readonly bool _isActive;

        private const string _defaultImagePath = "pack://application:,,,/Shopfloor;component/Resources/userDefault.png";

        public int Id => _id;
        public string Username => _username;
        public string Image => _image;
        public string Name => _name;
        public string Surname => _surname;
        public string FullName => $"{_name} {_surname}";
        public bool IsActive => _isActive;

        public User(int id, string username, string name, string surname, string imagePath, bool isActive)
        {
            _id = id;
            _username = username;
            _name = name;
            _surname = surname;
            _roles = new();
            _image = imagePath.Length > 0 ? imagePath : _defaultImagePath;
            _isActive = isActive;
        }

        public User(string username)
        {
            _username = username;
            _name = string.Empty;
            _surname = string.Empty;
            _roles = new();
            _image = _defaultImagePath;
            _isActive = true;
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
            _roles.Add(role);
        }
        public bool IsAuthorized(int roleValue)
        {
            return _roles.Any(role => role.Value == roleValue);
        }
    }
}