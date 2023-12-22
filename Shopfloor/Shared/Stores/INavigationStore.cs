using PrzegladyRemonty.Shared.ViewModels;

namespace PrzegladyRemonty.Shared.Stores
{
    public interface INavigationStore
    {
        ViewModelBase CurrentViewModel
        {
            set;
        }
    }
}