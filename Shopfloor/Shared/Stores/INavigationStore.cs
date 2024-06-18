using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Shared.Stores
{
    internal interface INavigationStore
    {
        ViewModelBase CurrentViewModel
        {
            set;
        }
    }
}