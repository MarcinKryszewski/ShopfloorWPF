using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Users.Add;
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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Admin.Users.List
{
    public class UsersListViewModel : ViewModelBase
    {
        private readonly IServiceProvider _database;
        private readonly ObservableCollection<User> _users = new();
        private readonly SelectedUserStore _selectedUser;
        private string _searchText = string.Empty;

        public ICollectionView Users => CollectionViewSource.GetDefaultView(_users);

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

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Users.Filter = FilterUsers;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public ICommand AddNewUserCommand { get; }
        public ICommand SetActivityUserCommand { get; }
        public ICommand EditUserCommand { get; }

        public UsersListViewModel(IServiceProvider mainServices, IServiceProvider databasServices)
        {
            _database = databasServices;
            UserProvider userProvider = databasServices.GetRequiredService<UserProvider>();

            Task.Run(() => LoadData(userProvider));

            _selectedUser = mainServices.GetRequiredService<SelectedUserStore>();

            AddNewUserCommand = new NavigateCommand<UsersAddViewModel>(mainServices.GetRequiredService<NavigationService<UsersAddViewModel>>());
            EditUserCommand = new NavigateCommand<UsersEditViewModel>(mainServices.GetRequiredService<NavigationService<UsersEditViewModel>>());
            SetActivityUserCommand = new UserSetActivityCommand(this, userProvider);
        }

        public async Task LoadData(UserProvider provider)
        {
            _users.Clear();
            IEnumerable<User> users = await _database.GetRequiredService<UserProvider>().GetAll();
            foreach (User user in users)
            {
                //await Task.Delay(350);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _users.Add(user);
                    OnPropertyChanged(nameof(Users));
                });
            }
        }

        public Task? UpdateUsers()
        {
            Users.Refresh();
            OnPropertyChanged(nameof(Users));
            return null;
        }

        private bool FilterUsers(object obj)
        {
            if (obj is User user)
            {
                return user.SearchValue.Contains(_searchText, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }
    }
}