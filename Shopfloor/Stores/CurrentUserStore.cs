using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ToastNotifications;
using ToastNotifications.Messages;

namespace Shopfloor.Stores
{
    internal sealed partial class CurrentUserStore
    {
        private readonly RoleProvider _roleProvider;
        private readonly RoleUserProvider _roleUserProvider;
        private readonly Notifier _notifier;
        private readonly UserValidation _userValidation;
        private bool _isUserLoggedIn;
        private User? _user;
        public CurrentUserStore(RoleProvider roleProvider, RoleUserProvider roleUserProvider, Notifier notifier)
        {
            _user = new("GOŚĆ");
            _isUserLoggedIn = false;
            _roleProvider = roleProvider;
            _roleUserProvider = roleUserProvider;
            _notifier = notifier;
            _userValidation = new(this);
            _propertyErrors = [];
        }
        public bool IsUserLoggedIn => _isUserLoggedIn;
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
            LoginNotification(_notifier);
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
            if (User == null) return [];
            if (User.Id is null) return [];

            IEnumerable<RoleUser> roleUsers = _roleUserProvider.GetAllForUser((int)User.Id).Result;

            return roleUsers;
        }
        public bool HasRole(Role role)
        {
            if (GetRoles().FirstOrDefault(role) is null) return false;
            return true;
        }
        public bool HasRole(int roleValue)
        {
            if (GetRoles().FirstOrDefault((r) => r.Value == roleValue) is null) return false;
            return true;
        }
        private void SetUserRoles(User user)
        {
            IEnumerable<Role> roles = GetRoles();
            IEnumerable<RoleUser> roleUsers = GetRoleUsers();

            foreach (RoleUser roleUser in roleUsers)
            {
                Role? role = roles.FirstOrDefault(r => r.Id == roleUser.RoleId);
                if (role == null) continue;
                user.AddRole(role);
            }
        }
        private static void LoginNotification(Notifier notifier) => notifier.ShowInformation("ZALOGOWANO POPRAWNIE");
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
                _notifier.ShowError("Nie udane logowanie automatyczne. Zaloguj się samodzielnie.");
                return;
            }

            _isUserLoggedIn = true;
            SetUserRoles(_user!);
            _notifier.ShowSuccess("Zalogowano automatycznie!");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUserLoggedIn)));
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
    }
}