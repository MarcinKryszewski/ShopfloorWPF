using Shopfloor.Features.Admin.Users.Stores;
using Shopfloor.Features.Admin.UsersList.Commands;
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Shopfloor.Features.Admin.Users
{
    internal sealed class UsersListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<User> _users = [];
        private readonly SelectedUserStore _selectedUser;
        private readonly IUserProvider _userProvider;
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
        public UsersListViewModel(INavigationService navigationService, IUserProvider userProvider, SelectedUserStore selectedUserStore)
        {
            _userProvider = userProvider;
            Task.Run(() => LoadData());

            _selectedUser = selectedUserStore;

            AddNewUserCommand = new NavigationCommand<UsersAddViewModel>(navigationService).Navigate();
            EditUserCommand = new NavigationCommand<UsersEditViewModel>(navigationService).Navigate();
            SetActivityUserCommand = new UserSetActivityCommand(this, _userProvider);
        }
        public async Task LoadData()
        {
            _users.Clear();
            IEnumerable<User> users = await _userProvider.GetAll();
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