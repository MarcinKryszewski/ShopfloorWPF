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
    internal sealed partial class CurrentUserStore
    {
        private const string DEFAULT_USERNAME = "GOŚĆ";
        private const string AUTOLOGIN_FAILED = "Nieudane logowanie automatyczne. Zaloguj się samodzielnie";
        private const string LOGIN_SUCCESSFUL = "ZALOGOWANO POPRAWNIE";
        private const string LOGIN_FAILED = "NIEUDANE LOGOWANIE";
        private readonly IProvider<Role> _roleProvider;
        private readonly IRoleUserProvider _roleUserProvider;
        private readonly INotifier _notifier;
        private readonly IAuthService _auth;
        private bool _isUserLoggedIn;
        private User? _user;
        public CurrentUserStore(IProvider<Role> roleProvider, IRoleUserProvider roleUserProvider, INotifier notifier, IAuthService auth)
        {
            _user = new(DEFAULT_USERNAME);
            _isUserLoggedIn = false;
            _roleProvider = roleProvider;
            _roleUserProvider = roleUserProvider;
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
            if (isAuto) return AUTOLOGIN_FAILED;
            return LOGIN_FAILED;
        }
        public void Logout()
        {
            _user = new(DEFAULT_USERNAME);

            _isUserLoggedIn = false;
            OnPropertyChanged(nameof(IsUserLoggedIn));
        }
        private IEnumerable<Role> GetRoles() => _roleProvider.GetAll().Result;
        private IEnumerable<RoleUser> GetRoleUsers()
        {
            if (User == null) return [];
            if (User.Id is null) return [];

            IEnumerable<RoleUser> roleUsers = _roleUserProvider.GetAllForUser((int)User.Id).Result;
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
        private void LoginSuccessNotification() => _notifier.ShowInformation(LOGIN_SUCCESSFUL);
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