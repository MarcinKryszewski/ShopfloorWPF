using System;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Mechanic.Errands.ErrandsEdit
{
    sealed internal class ErrandsEditViewModel : ViewModelBase
    {
        private readonly IServiceProvider _mainServices;
        private readonly IServiceProvider _databaseServices;

        public ErrandsEditViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            _mainServices = mainServices;
            _databaseServices = databaseServices;
        }
    }
}
