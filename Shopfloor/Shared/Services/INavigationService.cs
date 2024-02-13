using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Shared.Services
{
    internal interface INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        void Navigate();
    }
}