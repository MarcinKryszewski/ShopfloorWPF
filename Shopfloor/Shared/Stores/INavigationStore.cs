using System;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Shared.Stores
{
    internal interface INavigationStore
    {
        public event Action? CurrentViewModelChanged;
        ViewModelBase? CurrentViewModel
        {
            get;
            set;
        }
    }
}