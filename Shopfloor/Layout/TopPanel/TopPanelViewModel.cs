using Shopfloor.Features.Login;
using Shopfloor.Features.Mechanic;
using Shopfloor.Layout.TopPanel.Commands;
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;
using System.ComponentModel;
using System.Windows.Input;

namespace Shopfloor.Layout.TopPanel
{
    internal sealed class TopPanelViewModel : ViewModelBase
    {
        private readonly ICurrentUserStore _userStore;
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
        public TopPanelViewModel(INavigationService navigationService, ICurrentUserStore userStore)
        {
            _userStore = userStore;
            _userStore.PropertyChanged += OnUserAuthenticated;
            NavigateLoginCommand = new NavigationCommand<LoginViewModel>(navigationService).Navigate();

            ICommand returnCommand = new NavigationCommand<MechanicDashboardViewModel>(navigationService).Navigate();
            LogoutCommand = new LogoutCommand(_userStore, returnCommand);
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