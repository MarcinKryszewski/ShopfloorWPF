using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Layout.TopPanel;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Layout.Content
{
    internal sealed class ContentViewModel : ViewModelBase
    {
        private readonly TopPanelViewModel _topPanelViewModel;
        private readonly NavigationStore _navigationStore;

        public TopPanelViewModel TopPanelViewModel => _topPanelViewModel;
        public ViewModelBase? Content => _navigationStore.CurrentViewModel;

        public ContentViewModel(IServiceProvider mainServices)
        {
            _topPanelViewModel = mainServices.GetRequiredService<TopPanelViewModel>();

            _navigationStore = mainServices.GetRequiredService<NavigationStore>();
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(Content));
        }
    }
}