using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Users.List;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Features.Admin.UsersList.Commands;
using Shopfloor.Interfaces;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Shopfloor.Features.Admin.Users.Add
{
    public class UsersAddViewModel : ViewModelBase, IInputForm<User>
    {
        private readonly IServiceProvider _database;
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        private readonly RolesStore _rolesValueStore;
        private string _name = string.Empty;
        private List<Role> _rolesStorage = [];
        private string _surname = string.Empty;
        private string _username = string.Empty;
        public UsersAddViewModel(IServiceProvider mainServices, IServiceProvider databasServices)
        {
            _database = databasServices;
            _rolesValueStore = new();
            SetRoles();
            BackToListCommand = new NavigateCommand<UsersListViewModel>(mainServices.GetRequiredService<NavigationService<UsersListViewModel>>());
            AddNewUserCommand = new UserAddCommand(
                this,
                _rolesValueStore,
                databasServices.GetRequiredService<UserProvider>(),
                databasServices.GetRequiredService<RoleUserProvider>()
            );
        }
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public ICommand AddNewUserCommand { get; }
        public ICommand BackToListCommand { get; }
        public bool HasErrors => _propertyErrors.Count != 0;
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
            if (propertyName is null) return;
            _propertyErrors.Remove(propertyName);
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName ?? string.Empty, null) ?? [];
        }
        public bool IsDataValidate => !HasErrors;
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
            _rolesStorage = new(_database.GetRequiredService<RoleProvider>().GetAll().Result);
            UpdateRoles();
        }
    }
}