using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Features.Admin.UsersList.Commands;
using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Admin.Users
{
    internal sealed class UsersAddViewModel : ViewModelBase, IInputForm<User>
    {
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        private readonly IProvider<Role> _roleProvider;
        private readonly RolesStore _rolesValueStore;
        private string _name = string.Empty;
        private List<Role> _rolesStorage = [];
        private string _surname = string.Empty;
        private string _username = string.Empty;
        public UsersAddViewModel(
            INavigationCommand<UsersListViewModel> navigationService,
            IProvider<User> userProvider,
            IProvider<RoleUser> roleIUserProvider,
            IProvider<Role> roleProvider)
        {
            _rolesValueStore = new();
            _roleProvider = roleProvider;

            SetRoles();
            BackToListCommand = navigationService.Navigate();
            AddNewUserCommand = new UserAddCommand(
                this,
                _rolesValueStore,
                userProvider,
                roleIUserProvider);
        }
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public ICommand AddNewUserCommand { get; }
        public ICommand BackToListCommand { get; }
        public bool HasErrors => _propertyErrors.Count != 0;
        public bool IsDataValidate => !HasErrors;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public ObservableCollection<RoleValue> Roles => _rolesValueStore.Roles;
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public void AddError(string propertyName, string errorMassage)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, []);
            }
            _propertyErrors[propertyName]?.Add(errorMassage);
            OnErrorsChanged(propertyName);
        }
        public void CleanForm()
        {
            Username = string.Empty;
            Name = string.Empty;
            Surname = string.Empty;
            UpdateRoles();
        }
        public void ClearErrors(string? propertyName)
        {
            if (propertyName is null)
            {
                return;
            }

            _propertyErrors.Remove(propertyName);
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
        }
        public void ReloadData()
        {
            throw new NotImplementedException();
        }
        public void UpdateRoles()
        {
            _rolesValueStore.ClearRoles();
            foreach (Role role in _rolesStorage)
            {
                _rolesValueStore.AddRole(role, false);
            }
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(IsDataValidate));
        }
        private void SetRoles()
        {
            _rolesStorage = new(_roleProvider.GetAll().Result);
            UpdateRoles();
        }
    }
}