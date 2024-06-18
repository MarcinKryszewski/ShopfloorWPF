using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Shared.Stores
{
    internal sealed class NavigationStore : INavigationStore
    {
        private ViewModelBase? _currentViewModel;

        public ViewModelBase? CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action? CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}