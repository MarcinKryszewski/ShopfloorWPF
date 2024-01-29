using System;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Features.Mechanic.Errands.ErrandsNew
{
    sealed internal class ErrandsNewViewModel : ViewModelBase
    {
        private readonly IServiceProvider _mainServices;
        private readonly IServiceProvider _databaseServices;

        public ErrandsNewViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            this._mainServices = mainServices;
            this._databaseServices = databaseServices;
        }
    }
}
