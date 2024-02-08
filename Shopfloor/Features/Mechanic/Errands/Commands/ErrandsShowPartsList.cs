
using System;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.ErrandPartsList;
using Shopfloor.Features.Mechanic.Errands.ErrandsNew;
using Shopfloor.Features.Mechanic.Errands.Interfaces;
using Shopfloor.Shared.Commands;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed class ErrandsShowPartsList : CommandBase
    {
        private readonly IPartsList _viewModel;
        private readonly IServiceProvider _mainServices;

        public ErrandsShowPartsList(IPartsList viewModel, IServiceProvider mainServices)
        {
            _viewModel = viewModel;
            _mainServices = mainServices;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.PartsList = _viewModel.PartsList is null ? _mainServices.GetRequiredService<ErrandPartsListViewModel>() : null;
        }
    }
}