using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Shopfloor.Stores
{
    internal sealed partial class CurrentUserStore
    {
        private readonly IServiceProvider _databaseServices;
        private readonly RoleProvider _roleProvider;
        private readonly RoleUserProvider _roleUserProvider;
        private readonly UserValidation _userValidation;
        private bool _isUserLoggedIn;
        private User? _user;
        public CurrentUserStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
            _user = new("GOŚĆ");
            _isUserLoggedIn = false;
            _roleProvider = databaseServices.GetRequiredService<RoleProvider>();
            _roleUserProvider = databaseServices.GetRequiredService<RoleUserProvider>();
            _userValidation = new(this);
            _propertyErrors = [];
        }
        public bool IsUserLoggedIn
        {
            get => _isUserLoggedIn;
        }
        public User? User => _user;

        public void Login(string username, UserProvider provider, IInputForm<User> inputForm)
        {
            _user = provider.GetByUsername(username.ToLower()).Result ?? null;
            _userValidation.ValidateLogin(_user, inputForm);

            if (inputForm.HasErrors)
            {
                return;
            }

            _isUserLoggedIn = true;
            SetUserRoles(_user!);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUserLoggedIn)));
        }
        public void Logout()
        {
            _user = new("GOŚĆ");

            _isUserLoggedIn = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUserLoggedIn)));
        }
        private IEnumerable<Role> GetRoles()
        {
            return _roleProvider.GetAll().Result;
        }
        private IEnumerable<RoleUser> GetRoleUsers()
        {
            if (User == null) return Enumerable.Empty<RoleUser>();
            if (User.Id is null) return Enumerable.Empty<RoleUser>();

            IEnumerable<RoleUser> roleUsers = _roleUserProvider.GetAllForUser((int)User.Id).Result;

            return roleUsers;
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
    }
    internal sealed partial class CurrentUserStore
    {
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
    }
    internal sealed partial class CurrentUserStore : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>?> _propertyErrors;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyErrors.Count != 0;
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
        }
    }
    internal sealed partial class CurrentUserStore : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}