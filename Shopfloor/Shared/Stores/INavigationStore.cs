using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Shared.Stores
{
    public interface INavigationStore
    {
        ViewModelBase CurrentViewModel
        {
            set;
        }
    }
}