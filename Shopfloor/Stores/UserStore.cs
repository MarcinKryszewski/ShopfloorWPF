using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Validators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Shopfloor.Stores
{
    public class UserStore : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private User? _user;
        private bool _isUserLoggedIn;
        //private string _errorMassage = string.Empty;
        private readonly RoleProvider _roleProvider;
        private readonly RoleUserProvider _roleUserProvider;
        private readonly UserValidation _userValidation;
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public bool IsUserLoggedIn
        {
            get => _isUserLoggedIn;
        }

        public User? User => _user;

        public bool HasErrors => _propertyErrors.Count != 0;

        public UserStore(IServiceProvider databaseServices)
        {
            _user = new("GOŚĆ");
            _isUserLoggedIn = false;
            _roleProvider = databaseServices.GetRequiredService<RoleProvider>();
            _roleUserProvider = databaseServices.GetRequiredService<RoleUserProvider>();
            _userValidation = new(this);
        }
        public void AutoLogin(string username, UserProvider provider)
        {
            _user = provider.GetByUsername(username.ToLower()).Result ?? null;
            _userValidation.ValidateAutoLogin(_user, _propertyErrors);
            if (HasErrors)
            {
                _propertyErrors.Remove("LoginFailed");
                return;
            }

            _isUserLoggedIn = true;
            SetUserRoles(_user!);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUserLoggedIn)));
        }
        public void Login(string username, UserProvider provider, IInputForm<User> inputForm)
        {
            _user = provider.GetByUsername(username.ToLower()).Result ?? null;
            _userValidation.ValidateLogin(_user, inputForm);
            if (inputForm.HasErrors)
            {
                //_propertyErrors.Remove("LoginError");
                return;
            }

            _isUserLoggedIn = true;
            //_errorMassage = string.Empty;
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
            //_errorMassage = string.Empty;
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorMassage)));
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
            if (User.Id is null) return Enumerable.Empty<RoleUser>();
            IEnumerable<RoleUser> roleUsers = _roleUserProvider.GetAllForUser((int)User.Id).Result;
            return roleUsers;
        }

        private IEnumerable<Role> GetRoles()
        {
            return _roleProvider.GetAll().Result;
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            throw new NotImplementedException();
        }
    }
}