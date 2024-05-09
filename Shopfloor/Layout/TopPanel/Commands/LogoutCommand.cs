using Shopfloor.Shared.Commands;
using Shopfloor.Stores;

namespace Shopfloor.Layout.TopPanel.Commands
{
    internal class LogoutCommand : CommandBase
    {
        private readonly ICurrentUserStore _userStore;
        private readonly RelayCommand _returnCommand;

        public LogoutCommand(ICurrentUserStore userStore, RelayCommand returnCommand)
        {
            _userStore = userStore;
            _returnCommand = returnCommand;
        }

        public override void Execute(object? parameter)
        {
            _userStore.Logout();
            _returnCommand.Execute(true);

            //NavigationService<MechanicDashboardViewModel> navigationService = _mainServices.GetRequiredService<NavigationService<MechanicDashboardViewModel>>();
            //navigationService.Navigate();
        }
    }
}