using Shopfloor.Shared.Stores;

namespace Shopfloor.Shared.ViewModels
{
    internal sealed class MainViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly INavigationStore _navigationStore;
        public MainViewModel(INavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
        }
        public ViewModelBase? CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public ViewModelBase? CurrentViewModel => _navigationStore.CurrentViewModel;
        public bool IsOpen => _modalNavigationStore.IsOpen;
        private void OnCurrentModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsOpen));
        }
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}