using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Login;
using Shopfloor.Layout.TopPanel.Commands;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Shopfloor.Layout.TopPanel
{
    internal sealed class TopPanelViewModel : ViewModelBase
    {
        private readonly CurrentUserStore _userStore;
        private User? User => _userStore.User;

        public string UserImagePath
        {
            get
            {
                if (User is null) return "pack://application:,,,/Shopfloor;component/Resources/userDefault.png";
                return User.Image;
            }
        }

        public bool IsLoggedIn => _userStore.IsUserLoggedIn;
        public string Username => IsLoggedIn ? $"Witaj {User?.Name}!" : "Zaloguj się!";

        public ICommand NavigateLoginCommand { get; }
        public ICommand LogoutCommand { get; }

        public TopPanelViewModel(IServiceProvider userServices, IServiceProvider mainServices)
        {
            _userStore = userServices.GetRequiredService<CurrentUserStore>();
            _userStore.PropertyChanged += OnUserAuthenticated;
            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(mainServices.GetRequiredService<NavigationService<LoginViewModel>>());
            LogoutCommand = new LogoutCommand(_userStore, mainServices);
        }

        private void OnUserAuthenticated(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_userStore.IsUserLoggedIn))
            {
                OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(IsLoggedIn));
                OnPropertyChanged(nameof(UserImagePath));
            }
        }
    }
}