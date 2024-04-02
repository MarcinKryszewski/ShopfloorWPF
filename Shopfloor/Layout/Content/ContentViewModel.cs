using Shopfloor.Layout.TopPanel;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Layout.Content
{
    internal sealed class ContentViewModel : ViewModelBase
    {
        private readonly TopPanelViewModel _topPanelViewModel;
        private readonly NavigationStore _navigationStore;

        public TopPanelViewModel TopPanelViewModel => _topPanelViewModel;
        public ViewModelBase? Content => _navigationStore.CurrentViewModel;

        public ContentViewModel(TopPanelViewModel topPanelViewModel, NavigationStore navigationStore)
        {
            _topPanelViewModel = topPanelViewModel;

            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(Content));
        }
    }
}