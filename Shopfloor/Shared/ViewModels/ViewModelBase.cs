using System.ComponentModel;
using System.Windows;
using Shopfloor.Services.AuthServices;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Dispatchers;
using Shopfloor.Shared.Dummies;

namespace Shopfloor.Shared.ViewModels
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly INotifier _notifier;
        private readonly IUserContext _userContext;
        private IDispatcherWrapper? _dispatcher;
        public ViewModelBase(ViewModelBaseDependecies? dependecies = null)
        {
            _notifier = dependecies?.Notifier ?? new NotifierDummy();
            _navigationService = dependecies?.NavigationService ?? new NavigationServiceDummy();
            _userContext = dependecies?.UserContext ?? new EmptyUserContext();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public IDispatcherWrapper DispatcherWrapper
        {
            get => _dispatcher ?? new DispatcherWrapper(Application.Current.Dispatcher);
            init => _dispatcher = value;
        }
        protected INavigationService NavigationService => _navigationService;
        protected INotifier Notifier => _notifier;
        protected IUserContext UserContext => _userContext;
        public virtual void Dispose()
        {
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}