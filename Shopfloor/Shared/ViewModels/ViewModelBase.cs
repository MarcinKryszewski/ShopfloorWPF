using System.ComponentModel;
using System.Windows;
using Shopfloor.Shared.Dispatchers;

namespace Shopfloor.Shared.ViewModels
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        private IDispatcherWrapper? _dispatcher;
        public event PropertyChangedEventHandler? PropertyChanged;
        public IDispatcherWrapper DispatcherWrapper
        {
            get => _dispatcher ?? new DispatcherWrapper(Application.Current.Dispatcher);
            init => _dispatcher = value;
        }
        public virtual void Dispose()
        {
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}