using System;
using Shopfloor.Features;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Shared.Stores
{
    internal sealed class NavigationStore : INavigationStore
    {
        private readonly WorkInProgressViewModel wipViewModel = new();
        private ViewModelBase? _currentViewModel;
        public event Action? CurrentViewModelChanged;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel ?? wipViewModel;
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}