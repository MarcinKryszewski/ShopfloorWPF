using Shopfloor.Models;
using Shopfloor.Services.Providers;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Shopfloor.Stores
{
    public class UserStore : INotifyPropertyChanged
    {
        private User _user;
        private bool _isUserLoggedIn;
        private string _errorMassage = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsUserLoggedIn
        {
            get => _isUserLoggedIn;
        }
        public string ErrorMassage
        {
            get => _errorMassage;
            set
            {
                _errorMassage = value;
            }
        }

        public User User => _user;

        public UserStore()
        {
            _user = new("GOŚĆ");
            _isUserLoggedIn = false;
        }

        public void Login(string username, UserProvider provider)
        {
            if (username.Length == 0)
            {
                _errorMassage = @$"Podaj nazwę użytkownika";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorMassage)));
                return;
            }

            try
            {
                _user = provider.GetByUsername(username).Result;
                _isUserLoggedIn = true;
                _errorMassage = string.Empty;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUserLoggedIn)));
            }
            catch (AggregateException)
            {
                _errorMassage = $"Nie znaleziono użytkownika" + Environment.NewLine + username;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorMassage)));
            }

        }

        public void Logout()
        {
            _user = new("GOŚĆ");
            _isUserLoggedIn = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUserLoggedIn)));
        }

        public void ResetError()
        {
            _errorMassage = string.Empty;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorMassage)));
        }
    }
}