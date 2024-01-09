using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Login.Commands;
using Shopfloor.Services.Providers;
using Shopfloor.Shared.ViewModels;
using Shopfloor.Stores;
using System;
using System.Windows.Input;

namespace Shopfloor.Features.Login
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username = "";

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(IServiceProvider databaseServices, IServiceProvider userProvider)
        {
            LoginCommand = new LoginCommand(
                databaseServices.GetRequiredService<UserProvider>(),
                userProvider.GetRequiredService<UserStore>(),
                this);
        }
    }
}