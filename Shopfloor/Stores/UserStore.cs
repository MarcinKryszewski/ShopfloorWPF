using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Stores
{
    public class UserStore : INotifyPropertyChanged
    {
        private User _user;
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

        public User User => _user;

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

            try
            {
                _user = provider.GetByUsername(username).Result;
                _isUserLoggedIn = true;
                _errorMassage = string.Empty;
                _ = SetUserRoles(_user);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUserLoggedIn)));
            }
            catch (AggregateException)
            {
                _errorMassage = $"Nie znaleziono użytkownika" + Environment.NewLine + username;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorMassage)));
            }

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

        private async Task SetUserRoles(User user)
        {
            IEnumerable<Role> roles = GetRoles();
            IEnumerable<RoleUser> roleUsers = GetRoleUsers();

            var roleTasks = roleUsers.Select(async roleUser =>
            {
                Role role = roles.First(r => r.Id == roleUser.RoleId);
                await Task.Run(() => user.AddRole(role));
            });

            await Task.WhenAll(roleTasks);
        }

        private IEnumerable<RoleUser> GetRoleUsers()
        {
            return _roleUserProvider.GetAllForUser(User.Id).Result;
        }
        private IEnumerable<Role> GetRoles()
        {
            return _roleProvider.GetAll().Result;
        }
    }
}