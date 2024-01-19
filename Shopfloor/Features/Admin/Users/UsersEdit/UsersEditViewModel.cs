using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Users.List;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Features.Admin.UsersList.Commands;
using Shopfloor.Interfaces;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Shopfloor.Features.Admin.Users.Edit
{
    public class UsersEditViewModel : ViewModelBase, IInputForm<User>
    {
        private readonly IServiceProvider _database;
        private readonly RolesStore _rolesValueStore;
        private string _username = string.Empty;
        private string _name = string.Empty;
        private string _surname = string.Empty;
        private string _errorMassage = string.Empty;
        private readonly SelectedUserStore _selectedUser;
        private readonly int _selectedUserId;

        public string Username => _username ?? string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }
        public User? SelectedUser => _selectedUser.SelectedUser;
        public string ErrorMassage
        {
            get => string.IsNullOrEmpty(_errorMassage) ? string.Empty : _errorMassage;
            set
            {
                _errorMassage = value;
                OnPropertyChanged(nameof(ErrorMassage));
                OnPropertyChanged(nameof(HasErrorVisibility));
            }
        }
        public Visibility HasErrorVisibility => string.IsNullOrEmpty(ErrorMassage) ? Visibility.Collapsed : Visibility.Visible;

        public ObservableCollection<RoleValue> Roles => _rolesValueStore.Roles;

        public ICommand BackToListCommand { get; }
        public ICommand EditUserCommand { get; }

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

        private void SetRoles()
        {
            IEnumerable<Role> roles = _database.GetRequiredService<RoleProvider>().GetAll().Result;
            IEnumerable<RoleUser> roleUsers = _database.GetRequiredService<RoleUserProvider>().GetAllForUser(_selectedUserId).Result;

            foreach (Role role in roles)
            {
                _rolesValueStore.AddRole(role, roleUsers.Any((r) => r.RoleId == role.Id));
            }
        }

        public void CleanForm()
        {
            Name = SelectedUser?.Name ?? string.Empty;
            Surname = SelectedUser?.Surname ?? string.Empty;
            SetRoles();
        }

        public bool IsDataValidate(User inputValue)
        {
            if (SelectedUser?.Id == 1) return false;
            return true;
        }

        public void ReloadData()
        {
            throw new NotImplementedException();
        }
    }
}