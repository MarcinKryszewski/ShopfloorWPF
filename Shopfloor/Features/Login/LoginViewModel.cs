using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Dashboard;
using Shopfloor.Features.Login.Commands;
using Shopfloor.Interfaces;
using Shopfloor.Models;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Shopfloor.Features.Login
{
    public class LoginViewModel : ViewModelBase, IInputForm<User>
    {
        private string _username = "";
        private readonly UserStore _userStore;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string ErrorMassage
        {
            get => string.IsNullOrEmpty(_userStore.ErrorMassage) ? string.Empty : _userStore.ErrorMassage;
            set
            {
                _userStore.ErrorMassage = value;
                OnPropertyChanged(nameof(ErrorMassage));
                OnPropertyChanged(nameof(HasErrorVisibility));
            }
        }
        public Visibility HasErrorVisibility => string.IsNullOrEmpty(ErrorMassage) ? Visibility.Collapsed : Visibility.Visible;

        public ICommand LoginCommand { get; }

        public LoginViewModel(IServiceProvider mainServices, IServiceProvider databaseServices, IServiceProvider userProvider)
        {
            ICommand NavigateDashboardCommand = new NavigateCommand<DashboardViewModel>(mainServices.GetRequiredService<NavigationService<DashboardViewModel>>());
            _userStore = userProvider.GetRequiredService<UserStore>();
            _userStore.PropertyChanged += OnUserLogin;
            _userStore.ResetError();
            LoginCommand = new LoginCommand(
                databaseServices.GetRequiredService<UserProvider>(),
                _userStore,
                this,
                NavigateDashboardCommand);
        }

        private void OnUserLogin(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_userStore.ErrorMassage))
            {
                OnPropertyChanged(nameof(ErrorMassage));
                OnPropertyChanged(nameof(HasErrorVisibility));
            }
        }

        public void CleanForm()
        {

        }

        public bool IsDataValidate(User inputValue)
        {
            return true;
        }
    }
}