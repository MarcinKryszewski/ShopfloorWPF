using System;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Mechanic.Errands.ErrandsList
{
    public class ErrandsListViewModel : ViewModelBase
    {
        private readonly IServiceProvider _mainServices;
        private readonly IServiceProvider _databaseServices;

        public ErrandsListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _mainServices = mainServices;
            _databaseServices = databaseServices;
        }
    }
}