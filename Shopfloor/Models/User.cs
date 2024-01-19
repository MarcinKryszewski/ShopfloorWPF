using Shopfloor.Interfaces;
using Shopfloor.Services.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models
{
    public class User : ISearchableModel
    {
        private readonly HashSet<Role> _roles;
        private readonly int? _id;
        private readonly string _username;
        private readonly string _name;
        private readonly string _surname;
        private readonly string _image;
        private bool _isActive;

        private const string _defaultImagePath = "pack://application:,,,/Shopfloor;component/Resources/userDefault.png";

        public int? Id => _id;
        public string Username => _username;
        public string Image => _image.Length > 0 ? _image : _defaultImagePath;
        public string Name => _name;
        public string Surname => _surname;
        public string FullName => $"{_name} {_surname}";
        public bool IsActive => _isActive;
        public string SearchValue => _username + _name + _surname;

        public User(
            int id,
            string username,
            string name,
            string surname,
            string imagePath,
            bool isActive)
        {
            _id = id;
            _username = username;
            _name = name;
            _surname = surname;
            _roles = new();
            _image = imagePath;
            _isActive = isActive;
        }

        public User(string username, string name, string surname, string imagePath, bool isActive)
        {
            _username = username;
            _name = name;
            _surname = surname;
            _roles = new();
            _image = imagePath;
            _isActive = isActive;
        }

        public User(string username)
        {
            _username = username;
            _name = string.Empty;
            _surname = string.Empty;
            _roles = new();
            _image = string.Empty;
            _isActive = true;
        }

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
        }

        public async Task<int> Add(UserProvider provider)
        {
            return await provider.Create(this);
        }
        public async Task Edit(UserProvider provider)
        {
            await provider.Update(this);
        }
        public async Task Delete(UserProvider provider)
        {
            if (Id is null) return;
            await provider.Delete((int)Id);
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