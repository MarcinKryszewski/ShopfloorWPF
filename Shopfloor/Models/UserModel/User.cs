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
        private readonly UserDTO _data;
        private const string DEFAULT_IMAGE_PATH = "pack://application:,,,/Shopfloor;component/Resources/userDefault.png";
        public int? Id
        {
            get => _data.Id;
            init => _data.Id = value;
        }
        public required string Username
        {
            get => _data.Username;
            set => _data.Username = value;
        }
        public string Image
        {
            get => _data.ImagePath.Length > 0 ? _data.ImagePath : DEFAULT_IMAGE_PATH;
            set => _data.ImagePath = value;
        }
        public string Name
        {
            get => _data.Name;
            set => _data.Name = value;
        }
        public string Surname
        {
            get => _data.Surname;
            set => _data.Surname = value;
        }
        public string FullName => $"{_data.Name} {_data.Surname}";
        public bool IsActive
        {
            get => _data.IsActive;
            set => _data.IsActive = value;
        }
        public User()
        {
            _data = new();
        }
        public void SetActive(bool isActive) => _data.IsActive = isActive;
        public void AddRole(Role role) => _data.Roles.Add(role);
        public void ClearRoles() => _data.Roles.Clear();
        public IReadOnlyCollection<Role> GetRoles() => _data.Roles.ToList();
        public bool IsAuthorized(int roleValue) => _data.Roles.Any(role => role.Value == roleValue);
    }
    internal sealed partial class User : IEquatable<User>
    {
        public bool Equals(User? other)
        {
            if (other == null) return false;
            if (Id == null && other.Id == null)
            {
                return Username == other.Username;
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