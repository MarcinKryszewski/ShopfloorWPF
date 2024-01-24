using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Dashboard;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.Services;
using Shopfloor.Stores;
using System;

namespace Shopfloor.Layout.TopPanel.Commands
{
    internal class LogoutCommand : CommandBase
    {
        private readonly CurrentUserStore _userStore;
        private readonly IServiceProvider _mainServices;

        public LogoutCommand(CurrentUserStore userStore, IServiceProvider mainServices)
        {
            _userStore = userStore;
            _mainServices = mainServices;
        }

        public override void Execute(object? parameter)
        {
            _userStore.Logout();

            NavigationService<DashboardViewModel> navigationService = _mainServices.GetRequiredService<NavigationService<DashboardViewModel>>();
            navigationService.Navigate();
        }
    }
}