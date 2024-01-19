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
using System.Windows;
using System.Windows.Input;

namespace Shopfloor.Features.Admin.Users.Add
{
    public class UsersAddViewModel : ViewModelBase, IInputForm<User>
    {
        private readonly IServiceProvider _database;
        private readonly RolesStore _rolesValueStore;
        private string _username = string.Empty;
        private string _name = string.Empty;
        private string _surname = string.Empty;
        private string _errorMassage = string.Empty;

        private List<Role> _rolesStorage = new();

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
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
        public ICommand AddNewUserCommand { get; }

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

        private void SetRoles()
        {
            _rolesStorage = new(_database.GetRequiredService<RoleProvider>().GetAll().Result);
            UpdateRoles();
        }

        public void UpdateRoles()
        {
            _rolesValueStore.ClearRoles();
            foreach (Role role in _rolesStorage)
            {
                _rolesValueStore.AddRole(role, false);
            }
        }

        public void CleanForm()
        {
            Username = "";
            Name = "";
            Surname = "";
            UpdateRoles();
        }

        public bool IsDataValidate(User inputValue)
        {
            return true;
        }

        public void ReloadData()
        {
            throw new NotImplementedException();
        }
    }
}