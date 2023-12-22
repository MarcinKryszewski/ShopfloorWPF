using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Shared.Services
{
    public interface INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        void Navigate();
    }
}