using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using Shopfloor.Shared.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shopfloor.Models.UserModel
{
    internal sealed partial class User : DataModel
    {
        private readonly HashSet<Role> _roles = [];
        private readonly UserDTO _data;
        private const string DEFAULT_IMAGE_PATH = "pack://application:,,,/Shopfloor;component/Resources/userDefault.png";
        public int? Id => _data.Id;
        public string Username => _data.Username;
        public string Image => _data.ImagePath.Length > 0 ? _data.ImagePath : DEFAULT_IMAGE_PATH;
        public string Name => _data.Name;
        public string Surname => _data.Surname;
        public string FullName => $"{_data.Name} {_data.Surname}";
        public bool IsActive => _data.IsActive;
        public User(
            int id,
            string username,
            string name,
            string surname,
            string imagePath = DEFAULT_IMAGE_PATH,
            bool isActive = true)
        {
            _data = new()
            {
                Id = id,
                Username = username,
                Name = name,
                Surname = surname,
                ImagePath = imagePath,
                IsActive = isActive
            };
        }
        public User(string username, string name, string surname, string imagePath, bool isActive)
        {
            _data = new()
            {
                Username = username,
                Name = name,
                Surname = surname,
                ImagePath = imagePath,
                IsActive = isActive
            };
        }
        public User(string username)
        {
            _data = new()
            {
                Username = username
            };
        }

        public void SetActive(bool isActive) => _data.IsActive = isActive;
        public void AddRole(Role role) => _roles.Add(role);
        public bool IsAuthorized(int roleValue) => _roles.Any(role => role.Value == roleValue);
    }
    internal sealed partial class User : IEquatable<User>
    {
        public bool Equals(User? other)
        {
            if (other == null) return false;
            if (Id == null && other.Id == null)
            {
                return FullName == other.FullName;
            }

            return Id == other.Id;
        }
        public override bool Equals(object? obj) => obj is User objUser && Equals(objUser);
        public override int GetHashCode()
        {
            if (Id != null) return Id.GetHashCode();
            return FullName.GetHashCode();
        }
    }
    internal sealed partial class User : ISearchableModel
    {
        public string SearchValue => _data.Username + _data.Name + _data.Surname;
    }
}