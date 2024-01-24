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
using System.Linq;
using System.Windows.Input;

namespace Shopfloor.Features.Admin.Users.Edit
{
    public class UsersEditViewModel : ViewModelBase, IInputForm<User>
    {
        private readonly IServiceProvider _database;
        private readonly Dictionary<string, List<string>?> _propertyErrors = [];
        private readonly RolesStore _rolesValueStore;
        private readonly SelectedUserStore _selectedUser;
        private readonly int _selectedUserId;
        private string _name = string.Empty;
        private string _surname = string.Empty;
        private string _username = string.Empty;
        public UsersEditViewModel(IServiceProvider mainServices, IServiceProvider databasServices)
        {
            _database = databasServices;
            _selectedUser = mainServices.GetRequiredService<SelectedUserStore>();
            _selectedUserId = _selectedUser.SelectedUser?.Id == null ? 0 : (int)_selectedUser.SelectedUser.Id;
            _rolesValueStore = new();

            FillForm();

            BackToListCommand = new NavigateCommand<UsersListViewModel>(mainServices.GetRequiredService<NavigationService<UsersListViewModel>>());

            string imagePath = _selectedUser.SelectedUser?.Image ?? string.Empty;
            bool isActive = _selectedUser.SelectedUser?.IsActive ?? false;
            EditUserCommand = new UserEditCommand(
                this,
                _database.GetRequiredService<UserProvider>(),
                _database.GetRequiredService<RoleUserProvider>(),
                _rolesValueStore,
                _selectedUserId,
                imagePath,
                isActive);
        }
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public ICommand BackToListCommand { get; }
        public ICommand EditUserCommand { get; }
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
        public User? SelectedUser => _selectedUser.SelectedUser;
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }
        public string Username => _username ?? string.Empty;
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
            Name = SelectedUser?.Name ?? string.Empty;
            Surname = SelectedUser?.Surname ?? string.Empty;
            SetRoles();
        }
        public void ClearErrors(string propertyName)
        {
            _propertyErrors.Remove(propertyName);
        }
        public void FillForm()
        {
            User? selectedUser = _selectedUser.SelectedUser;
            if (selectedUser is not null)
            {
                _username = selectedUser.Username;
                _name = selectedUser.Name;
                _surname = selectedUser.Surname;
                SetRoles();
                return;
            }
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName ?? "", null) ?? [];
        }
        public void ReloadData()
        {
            throw new NotImplementedException();
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(IsDataValidate));
        }
        private void SetRoles()
        {
            IEnumerable<Role> roles = _database.GetRequiredService<RoleProvider>().GetAll().Result;
            IEnumerable<RoleUser> roleUsers = _database.GetRequiredService<RoleUserProvider>().GetAllForUser(_selectedUserId).Result;

            foreach (Role role in roles)
            {
                _rolesValueStore.AddRole(role, roleUsers.Any((r) => r.RoleId == role.Id));
            }
        }
    }
}