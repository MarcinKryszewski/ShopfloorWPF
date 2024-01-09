using Shopfloor.Models;
using Shopfloor.Services.Providers;
using System.ComponentModel;

namespace Shopfloor.Stores
{
    public class UserStore : INotifyPropertyChanged
    {
        private User _user;
        private bool _isUserLoggedIn;

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsUserLoggedIn
        {
            get => _isUserLoggedIn;
        }

        public User User => _user;

        public UserStore()
        {
            _user = new("GOŚĆ");
            _isUserLoggedIn = false;
        }

        public void Login(string username, UserProvider provider)
        {
            _user = provider.GetByUsername(username).Result;
            _isUserLoggedIn = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUserLoggedIn)));
        }
        public void Logout()
        {
            _user = new("GOŚĆ");
            _isUserLoggedIn = false;
        }
    }
}