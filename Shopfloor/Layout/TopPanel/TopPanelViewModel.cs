using System;
using System.ComponentModel;
using Shopfloor.Services.AuthServices;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Layout.TopPanel
{
    internal sealed class TopPanelViewModel : ViewModelBase
    {
        private readonly IUserContext _userContext;
        public TopPanelViewModel(
            INavigationService navigationService,
            IUserContext userContext)
        {
            _userContext = userContext;
            _userContext.PropertyChanged += OnUserAuthenticated;
        }
        public string UsernameTitle => _userContext.UserPrompt;
        private void OnUserAuthenticated(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_userContext.Person) || e.PropertyName == nameof(_userContext.IsAuthenticated))
            {
                OnPropertyChanged(nameof(UsernameTitle));
            }
        }
    }
}