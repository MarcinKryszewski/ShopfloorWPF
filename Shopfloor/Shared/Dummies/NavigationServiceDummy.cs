using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Shared.Dummies
{
    internal class NavigationServiceDummy : INavigationService
    {
        public void NavigateTo<T>()
            where T : ViewModelBase
        {
        }
    }
}