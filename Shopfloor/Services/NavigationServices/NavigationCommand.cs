using System.Windows.Input;
using Shopfloor.Shared.Commands;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Services.NavigationServices
{
    internal class NavigationCommand<T> : INavigationCommand<T> where T : ViewModelBase
    {
        private INavigationService _navigationService;
        public NavigationCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public ICommand Navigate()
        {
            return new RelayCommand(o => { _navigationService.NavigateTo<T>(); }, o => true);
        }
    }
}