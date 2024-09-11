using System;
using Shopfloor.Features.WorkInProgressFeature;
using Shopfloor.Shared;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Services.NavigationServices
{
    internal sealed class NavigationService : ObservableObject, INavigationService
    {
        private readonly INavigationStore _navigationStore;
        private readonly Func<Type, ViewModelBase> _viewModelFactory;
        public NavigationService(INavigationStore navigationStore, Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
            _navigationStore = navigationStore;
        }
        public void NavigateTo<TViewModel>()
            where TViewModel : ViewModelBase
        {
            ViewModelBase viewModel = CreateViewModel<TViewModel>();
            _navigationStore.CurrentViewModel = viewModel;
        }

        public ViewModelBase CreateViewModel<TViewModel>()
            where TViewModel : ViewModelBase
        {
            try
            {
                return _viewModelFactory.Invoke(typeof(TViewModel));
            }
            catch (Exception e)
            {
                return _viewModelFactory.Invoke(typeof(WorkInProgressViewModel));
            }
        }
    }
}