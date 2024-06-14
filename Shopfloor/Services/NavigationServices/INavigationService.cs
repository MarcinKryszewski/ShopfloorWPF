using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Services.NavigationServices
{
    internal interface INavigationService
    {
        void NavigateTo<T>() where T : ViewModelBase;
    }
}