using System.ComponentModel;
using System.Windows.Input;
using Shopfloor.Features.Login;
using Shopfloor.Features.Mechanic;
using Shopfloor.Layout.TopPanel.Commands;
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;

namespace Shopfloor.Layout.TopPanel
{
    internal sealed class TopPanelViewModel : ViewModelBase
    {
        private readonly ICurrentUserStore _userStore;
        public TopPanelViewModel(INavigationService navigationService, ICurrentUserStore userStore)
        {
            _userStore = userStore;
            _userStore.PropertyChanged += OnUserAuthenticated;
            NavigateLoginCommand = new NavigationCommand<LoginViewModel>(navigationService).Navigate();

            ICommand returnCommand = new NavigationCommand<MechanicDashboardViewModel>(navigationService).Navigate();
            LogoutCommand = new LogoutCommand(_userStore, returnCommand);
        }
        public bool IsLoggedIn => _userStore.IsUserLoggedIn;
        public ICommand LogoutCommand { get; }
        public ICommand NavigateLoginCommand { get; }
        public string UserImagePath
        {
            get
            {
                if (User is null)
                {
                    return "pack://application:,,,/Shopfloor;component/Resources/userDefault.png";
                }

                return User.Image;
            }
        }
        public string Username => IsLoggedIn ? $"Witaj {User?.Name}!" : "Zaloguj się!";
        private User? User => _userStore.User;
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