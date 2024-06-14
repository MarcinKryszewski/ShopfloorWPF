using System.Windows.Input;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Services.NavigationServices
{
    internal interface INavigationCommand<T> where T : ViewModelBase
    {
        public ICommand Navigate();
    }
}