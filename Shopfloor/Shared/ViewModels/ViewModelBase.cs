using System.ComponentModel;
using System.Windows;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Services.NotificationServices;
using Shopfloor.Shared.Dispatchers;
using Shopfloor.Shared.Dummies;

namespace Shopfloor.Shared.ViewModels
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        private readonly INotifier _notifier;
        private readonly INavigationService _navigationService;
        private IDispatcherWrapper? _dispatcher;
        public ViewModelBase(ViewModelBaseDependecies? dependecies = null)
        {
            _notifier = dependecies?.Notifier ?? new NotifierDummy();
            _navigationService = dependecies?.NavigationService ?? new NavigationServiceDummy();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public IDispatcherWrapper DispatcherWrapper
        {
            get => _dispatcher ?? new DispatcherWrapper(Application.Current.Dispatcher);
            init => _dispatcher = value;
        }
        protected INotifier Notifier => _notifier;
        protected INavigationService NavigationService => _navigationService;
        public virtual void Dispose()
        {
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}