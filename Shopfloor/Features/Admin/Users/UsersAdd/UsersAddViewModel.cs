using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Users.List;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Features.Admin.UsersList.Commands;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Admin.Users.Add
{
    public class UsersAddViewModel : ViewModelBase
    {
        private readonly IServiceProvider _database;
        private readonly RolesStore _rolesValueStore;
        private readonly ObservableCollection<RoleValue> _roles;
        private string _username = string.Empty;
        private string _name = string.Empty;
        private string _surname = string.Empty;

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

        public IEnumerable<RoleValue> Roles => _roles;

        public ICommand BackToListCommand { get; }
        public ICommand AddNewUserCommand { get; }

        public UsersAddViewModel(IServiceProvider mainServices, IServiceProvider databasServices)
        {
            _database = databasServices;
            _rolesValueStore = new();
            SetRoles();
            _roles = _rolesValueStore.Roles;
            BackToListCommand = new NavigateCommand<UsersListViewModel>(mainServices.GetRequiredService<NavigationService<UsersListViewModel>>());
            AddNewUserCommand = new UserAddCommand(
                this,
                _rolesValueStore,
                databasServices.GetRequiredService<UserProvider>(),
                databasServices.GetRequiredService<RoleUserProvider>(),
                databasServices.GetRequiredService<RoleProvider>()
            );
        }

        private void SetRoles()
        {
            List<Role> roles = new(_database.GetRequiredService<RoleProvider>().GetAll().Result);
            foreach (Role role in roles)
            {
                _rolesValueStore.AddRole(role, false);
            }
        }
    }
}