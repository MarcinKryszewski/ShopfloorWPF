using PrzegladyRemonty.Shared.ViewModels;

namespace PrzegladyRemonty.Shared.Services
{
    public interface INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        void Navigate();
    }
}