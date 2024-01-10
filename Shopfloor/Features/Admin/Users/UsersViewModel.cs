using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Admin.Users
{
    public class UsersViewModel : ViewModelBase
    {
        private IServiceProvider _database;
        private ObservableCollection<User> _users;

        //public IEnumerable<User> Users => _users;
        public ICollectionView Users { get; }

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

        /*public void UpdateLines(IEnumerable<Line> lines)
        {
            _lines.Clear();
            foreach (Line line in lines)
            {
                _lines.Add(line);
            }
        }*/
    }
}