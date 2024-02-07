using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.ErrandsNew;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandStatuses;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Stores;
using System;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed partial class ErrandNewCommand : CommandBase
    {
        private readonly ErrandsNewViewModel _viewModel;
        private readonly IServiceProvider _databaseServices;
        private readonly ErrandProvider _errandProvider;
        private readonly CurrentUserStore _currentUser;
        private readonly SelectedErrandStore _currentErrand;
        private readonly ErrandPartProvider _errandPartProvider;
        private readonly ErrandStatusProvider _errandStatus;
        public ErrandNewCommand(ErrandsNewViewModel errandsNewViewModel, IServiceProvider databaseServices, CurrentUserStore currentUser, SelectedErrandStore currentErrand)
        {
            _viewModel = errandsNewViewModel;
            _databaseServices = databaseServices;
            _errandProvider = _databaseServices.GetRequiredService<ErrandProvider>();
            _errandPartProvider = _databaseServices.GetRequiredService<ErrandPartProvider>();
            _errandStatus = _databaseServices.GetRequiredService<ErrandStatusProvider>();
            _currentUser = currentUser;
            _currentErrand = currentErrand;
        }
        public override void Execute(object? parameter)
        {
            bool partsOrdered = false;
            if (_viewModel.IsDataValidate)
            {
                ErrandDTO errandDTO = _viewModel.ErrandDTO;

                Errand errand = new(DateTime.Now, _currentUser.User?.Id, errandDTO.Machine?.Id, errandDTO.ErrandType?.Id, errandDTO.Description ?? "BRAK OPISU", errandDTO.Priority)
                {
                    ExpectedDate = errandDTO.ExpectedDate,
                    Responsible = errandDTO.Responsible,
                    SapNumber = errandDTO.SapNumber
                };

                _currentErrand.ErrandId = _errandProvider.Create(errand).Result;

                foreach (ErrandPart errandPart in _currentErrand.ErrandParts)
                {
                    _ = _errandPartProvider.Create(new ErrandPart((int)_currentErrand.ErrandId, errandPart.PartId, errandPart.Amount));
                    partsOrdered = true;
                }
                _ = _errandStatus.Create(new ErrandStatus(
                    (int)_currentErrand.ErrandId,
                    ErrandStatusList.NoPartsList,
                    DateTime.Now
                ));

                if (partsOrdered)
                {
                    ErrandStatus errandStatus = new((int)_currentErrand.ErrandId, ErrandStatusList.PartsListCompleted, DateTime.Now);
                    _ = _errandStatus.Create(errandStatus);
                }

                _viewModel.ReloadData();
                _viewModel.CleanForm();
            }
        }
    }
}