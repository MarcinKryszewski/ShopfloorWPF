using System;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Shared.Stores
{
    internal interface INavigationStore
    {
        ViewModelBase CurrentViewModel
        {
            get;
            set;
        }
        public event Action? CurrentViewModelChanged;
    }
}