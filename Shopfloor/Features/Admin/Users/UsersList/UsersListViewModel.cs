using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Users.Add;
using Shopfloor.Features.Admin.Users.Commands;
using Shopfloor.Features.Admin.Users.Edit;
using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Features.Admin.UsersList.Commands;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Admin.Users.List
{
    public class UsersListViewModel : ViewModelBase
    {
        private readonly IServiceProvider _database;
        private readonly ObservableCollection<User> _users;
        private readonly SelectedUserStore _selectedUser;

        public ICollectionView Users { get; }
        public User? SelectedUser
        {
            get => _selectedUser.SelectedUser;
            set
            {
                if (_selectedUser.SelectedUser != value)
                {
                    _selectedUser.SelectedUser = value;
                    OnPropertyChanged(nameof(SelectedUser));
                }
            }
        }

        public ICommand AddNewUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand ActivateUserCommand { get; }
        public ICommand EditUserCommand { get; }

        public UsersListViewModel(IServiceProvider mainServices, IServiceProvider databasServices)
        {
            _database = databasServices;
            _users = GetUsers();
            _selectedUser = mainServices.GetRequiredService<SelectedUserStore>();
            Users = CollectionViewSource.GetDefaultView(_users);

            AddNewUserCommand = new NavigateCommand<UsersAddViewModel>(mainServices.GetRequiredService<NavigationService<UsersAddViewModel>>());
            EditUserCommand = new NavigateCommand<UsersEditViewModel>(mainServices.GetRequiredService<NavigationService<UsersEditViewModel>>());

            UserProvider userProvider = databasServices.GetRequiredService<UserProvider>();
            DeleteUserCommand = new UserDeleteCommand(this, userProvider);
            ActivateUserCommand = new UserActivateCommand(this, userProvider);
        }

        private ObservableCollection<User> GetUsers()
        {
            IEnumerable<User> users = _database.GetRequiredService<UserProvider>().GetAll().Result;
            return new ObservableCollection<User>(users);

        }

        /*public void UpdateUsers(IEnumerable<User> users)
        {
            _users.Clear();
            foreach (User user in users)
            {
                _users.Add(user);
            }
        }*/

        public void UpdateUsers()
        {
            _users.Clear();
            foreach (User user in GetUsers())
            {
                _users.Add(user);
            }
            OnPropertyChanged(nameof(Users));
        }
    }
}