using Shopfloor.Layout.TopPanel;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Layout.Content
{
    internal sealed class ContentViewModel : ViewModelBase
    {
        private readonly INavigationStore _navigationStore;
        private readonly TopPanelViewModel _topPanelViewModel;
        public ContentViewModel(TopPanelViewModel topPanelViewModel, INavigationStore navigationStore)
        {
            _topPanelViewModel = topPanelViewModel;

            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }
        public ViewModelBase? Content => _navigationStore.CurrentViewModel;
        public TopPanelViewModel TopPanelViewModel => _topPanelViewModel;
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(Content));
        }
    }
}