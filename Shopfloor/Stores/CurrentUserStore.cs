using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Services;
using Shopfloor.Services.NotificationServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Shopfloor.Stores
{
    internal sealed partial class CurrentUserStore : ICurrentUserStore
    {
        private const string _defaultUsername = "GOŚĆ";
        private const string _autologinFailed = "Nieudane logowanie automatyczne. Zaloguj się samodzielnie";
        private const string _loginSuccessfull = "ZALOGOWANO POPRAWNIE";
        private const string _loginFailed = "NIEUDANE LOGOWANIE";
        private readonly IProvider<Role> _roleProvider;
        private readonly RoleUserProvider _roleIUserProvider;
        private readonly INotifier _notifier;
        private readonly IAuthService _auth;
        private bool _isUserLoggedIn;
        private User? _user;
        public CurrentUserStore(IProvider<Role> roleProvider, IProvider<RoleUser> roleIUserProvider, INotifier notifier, IAuthService auth)
        {
            _user = new() { Username = _defaultUsername };
            _isUserLoggedIn = false;
            _roleProvider = roleProvider;
            _roleIUserProvider = (RoleUserProvider)roleIUserProvider;
            _notifier = notifier;
            _auth = auth;
            _propertyErrors = [];
        }
        public bool IsUserLoggedIn => _isUserLoggedIn;
        public User? User => _user;
        public void Login(string username, bool isAuto = false)
        {
            _user = _auth.Login(username);

            if (_user is null)
            {
                _notifier.ShowError(GetFailedText(isAuto));
                return;
            }

            _isUserLoggedIn = true;

            LoginSuccessNotification();
            UserLoginStatusChanged();
        }
        private static string GetFailedText(bool isAuto)
        {
            if (isAuto) return _autologinFailed;
            return _loginFailed;
        }
        public void Logout()
        {
            _user = new() { Username = _defaultUsername };

            _isUserLoggedIn = false;
            OnPropertyChanged(nameof(IsUserLoggedIn));
        }
        private IEnumerable<Role> GetRoles() => _roleProvider.GetAll().Result;
        private IEnumerable<RoleUser> GetRoleUsers()
        {
            if (User == null) return [];
            if (User.Id is null) return [];

            IEnumerable<RoleUser> roleUsers = _roleIUserProvider.GetAllForUser((int)User.Id).Result;
            return roleUsers;
        }
        private void SetUserRoles(User? user)
        {
            if (user is null) return;

            IEnumerable<Role> roles = GetRoles();
            IEnumerable<RoleUser> roleUsers = GetRoleUsers();

            foreach (RoleUser roleUser in roleUsers)
            {
                Role? role = roles.FirstOrDefault(r => r.Id == roleUser.RoleId);
                if (role == null) continue;
                user.AddRole(role);
            }
        }
        private void LoginSuccessNotification() => _notifier.ShowInformation(_loginSuccessfull);
        private void UserLoginStatusChanged()
        {
            SetUserRoles(_user);
            OnPropertyChanged(nameof(IsUserLoggedIn));
        }
    }
    internal sealed partial class CurrentUserStore : INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>?> _propertyErrors;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _propertyErrors.Count != 0;
        public IEnumerable GetErrors(string? propertyName) => _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
    }
    internal sealed partial class CurrentUserStore : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}