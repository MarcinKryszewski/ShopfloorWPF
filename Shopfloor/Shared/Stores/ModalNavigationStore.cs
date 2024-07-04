using System;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Shared.Stores
{
    internal sealed class ModalNavigationStore : INavigationStore
    {
        private ViewModelBase? _currentViewModel;

        public event Action? CurrentViewModelChanged;
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

        public bool IsOpen => CurrentViewModel != null;
        public void Close()
        {
            CurrentViewModel = null;
        }
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}