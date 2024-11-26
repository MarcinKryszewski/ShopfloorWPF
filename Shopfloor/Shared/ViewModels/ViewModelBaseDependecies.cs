using Shopfloor.Contexts;
using Shopfloor.Services.AuthServices;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Services.NotificationServices;

namespace Shopfloor.Shared.ViewModels
{
    internal class ViewModelBaseDependecies
    {
        private readonly INavigationService _navigationService;
        private readonly INotifier _notifier;
        private readonly IUserContext _userContext;
        public ViewModelBaseDependecies(
            INotifier notifier,
            INavigationService navigationService,
            IUserContext userContext)
        {
            _notifier = notifier;
            _navigationService = navigationService;
            _userContext = userContext;
        }
        public INavigationService NavigationService => _navigationService;
        public INotifier Notifier => _notifier;
        public IUserContext UserContext => _userContext;
    }
}