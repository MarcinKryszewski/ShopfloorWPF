using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using System.Collections.Generic;
using System.Linq;

namespace Shopfloor.Models.UserModel
{
    public class User : ISearchableModel
    {
        private readonly HashSet<Role> _roles = [];
        private readonly int? _id;
        private readonly string _username = string.Empty;
        private readonly string _name = string.Empty;
        private readonly string _surname = string.Empty;
        private readonly string _image = _defaultImagePath;
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
            string imagePath = _defaultImagePath,
            bool isActive = true)
        {
            _id = id;
            _username = username;
            _name = name;
            _surname = surname;
            _image = imagePath;
            _isActive = isActive;
        }
        public User(string username, string name, string surname, string imagePath, bool isActive)
        {
            _username = username;
            _name = name;
            _surname = surname;
            _image = imagePath;
            _isActive = isActive;
        }
        public User(string username)
        {
            _username = username;
        }
        public void SetActive(bool isActive)
        {
            _isActive = isActive;
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