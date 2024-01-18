using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Shopfloor.Stores
{
    public class UserStore : INotifyPropertyChanged
    {
        private User? _user;
        private bool _isUserLoggedIn;
        private string _errorMassage = string.Empty;
        private readonly RoleProvider _roleProvider;
        private readonly RoleUserProvider _roleUserProvider;

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsUserLoggedIn
        {
            get => _isUserLoggedIn;
        }
        public string ErrorMassage
        {
            get => _errorMassage;
            set
            {
                _errorMassage = value;
            }
        }

        public User? User => _user;

        public UserStore(IServiceProvider databaseServices)
        {
            _user = new("GOŚĆ");
            _isUserLoggedIn = false;
            _roleProvider = databaseServices.GetRequiredService<RoleProvider>();
            _roleUserProvider = databaseServices.GetRequiredService<RoleUserProvider>();
        }

        public void Login(string username, UserProvider provider)
        {
            if (username.Length == 0)
            {
                _errorMassage = @$"Podaj nazwę użytkownika";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorMassage)));
                return;
            }


            _user = provider.GetByUsername(username.ToLower()).Result ?? null;

            if (_user is null)
            {
                _errorMassage = $"Nie znaleziono użytkownika" + Environment.NewLine + username;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorMassage)));
                return;
            }

            _isUserLoggedIn = true;
            _errorMassage = string.Empty;
            SetUserRoles(_user);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUserLoggedIn)));

        }

        public void Logout()
        {
            _user = new("GOŚĆ");
            _isUserLoggedIn = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUserLoggedIn)));
        }

        public void ResetError()
        {
            _errorMassage = string.Empty;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorMassage)));
        }

        private void SetUserRoles(User user)
        {
            IEnumerable<Role> roles = GetRoles();
            IEnumerable<RoleUser> roleUsers = GetRoleUsers();

            foreach (RoleUser roleUser in roleUsers)
            {
                Role role = roles.First(r => r.Id == roleUser.RoleId);
                user.AddRole(role);
            }
        }

        private IEnumerable<RoleUser> GetRoleUsers()
        {
            if (User == null) return Enumerable.Empty<RoleUser>();
            IEnumerable<RoleUser> roleUsers = _roleUserProvider.GetAllForUser(User.Id).Result;
            return roleUsers;
        }
        private IEnumerable<Role> GetRoles()
        {
            return _roleProvider.GetAll().Result;
        }
    }
}