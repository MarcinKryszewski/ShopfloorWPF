﻿using Shopfloor.Shared;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Services.NavigationServices
{
    internal sealed class NavigationService : ObservableObject, INavigationService
    {
        private readonly Func<Type, ViewModelBase> _viewModelFactory;
        private readonly INavigationStore _navigationStore;
        public NavigationService(INavigationStore navigationStore, Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
            _navigationStore = navigationStore;
        }
        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            ViewModelBase viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            _navigationStore.CurrentViewModel = viewModel;
        }
    }
}