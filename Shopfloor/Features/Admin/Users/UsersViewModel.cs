using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Shopfloor.Features.Admin.Users
{
    public class UsersViewModel : ViewModelBase
    {
        private readonly IServiceProvider _database;
        private readonly ObservableCollection<User> _users;
        private User? _selectedUser;

        public ICollectionView Users { get; }
        public User? SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged(nameof(SelectedUser));
                }
            }
        }

        public UsersViewModel(IServiceProvider databasServices)
        {
            _database = databasServices;
            _users = GetUsers();
            Users = CollectionViewSource.GetDefaultView(_users);
        }

        private ObservableCollection<User> GetUsers()
        {
            IEnumerable<User> users = _database.GetRequiredService<UserProvider>().GetAll().Result;
            return new ObservableCollection<User>(users);

        }

        public void UpdateUers(IEnumerable<User> users)
        {
            _users.Clear();
            foreach (User user in users)
            {
                _users.Add(user);
            }
        }
    }
}