using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Shared.Services
{
    public class NavigationService<TViewModel> : INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly INavigationStore _navigationStore;
        private readonly CreateViewModel<TViewModel> _createViewModel;

        public NavigationService(INavigationStore navigationStore, CreateViewModel<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}