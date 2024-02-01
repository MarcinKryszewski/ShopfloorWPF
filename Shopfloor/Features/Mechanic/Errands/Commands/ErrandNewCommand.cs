using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.ErrandsNew;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Stores;
using System;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed partial class ErrandNewCommand : CommandBase
    {
        private ErrandsNewViewModel _viewModel;
        private ErrandProvider _errandProvider;
        private readonly CurrentUserStore _currentUser;

        public ErrandNewCommand(ErrandsNewViewModel errandsNewViewModel, IServiceProvider databaseServices, CurrentUserStore currentUser)
        {
            _viewModel = errandsNewViewModel;
            _errandProvider = databaseServices.GetRequiredService<ErrandProvider>();
            _currentUser = currentUser;
        }

        public override void Execute(object? parameter)
        {
            if (_viewModel.IsDataValidate)
            {
                ErrandDTO errandDTO = _viewModel.ErrandDTO;

                Errand errand = new(
                    DateTime.Now,
                    _currentUser.User?.Id,
                    errandDTO.Machine?.Id,
                    errandDTO.ErrandType?.Id,
                    errandDTO.Description,
                    errandDTO.Priority)
                {
                    ExpectedDate = errandDTO.ExpectedDate,
                    Responsible = errandDTO.Responsible,
                    SapNumber = errandDTO.SapNumber
                };

                int newErrandId = _errandProvider.Create(errand).Result;
            }
        }
    }
}