using Shopfloor.Services.NavigationServices;
using Shopfloor.Services.NotificationServices;

namespace Shopfloor.Shared.ViewModels
{
    internal class ViewModelBaseDependecies
    {
        private readonly INavigationService _navigationService;
        private readonly INotifier _notifier;
        public ViewModelBaseDependecies(INotifier notifier, INavigationService navigationService)
        {
            _notifier = notifier;
            _navigationService = navigationService;
        }
        public INavigationService NavigationService => _navigationService;
        public INotifier Notifier => _notifier;
    }
}