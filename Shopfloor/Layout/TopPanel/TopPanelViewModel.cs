using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Login;
using Shopfloor.Layout.TopPanel.Commands;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Shopfloor.Layout.TopPanel
{
    public class TopPanelViewModel : ViewModelBase
    {
        private readonly string _userImagePath;
        private readonly UserStore _userStore;

        public string UserImagePath => _userImagePath;
        public bool IsLoggedIn => _userStore.IsUserLoggedIn;
        public string Username => _userStore.User.Username;

        public ICommand NavigateLoginCommand { get; }
        public ICommand LogoutCommand { get; }

        public TopPanelViewModel(IServiceProvider userServices, IServiceProvider mainServices)
        {
            _userStore = userServices.GetRequiredService<UserStore>();
            _userStore.PropertyChanged += OnUserAuthenticated;
            _userImagePath = _userStore.User.Image;
            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(mainServices.GetRequiredService<NavigationService<LoginViewModel>>());
            LogoutCommand = new LogoutCommand();
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
