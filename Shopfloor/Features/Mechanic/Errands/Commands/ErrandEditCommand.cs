using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.ErrandsEdit;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed class ErrandEditCommand : CommandBase
    {
        private readonly ErrandsEditViewModel _viewModel;
        private readonly IServiceProvider _databaseServices;
        private readonly CurrentUserStore _currentUser;
        private readonly SelectedErrandStore _currentErrand;
        private readonly ErrandProvider _errandProvider;
        private readonly ErrandPartProvider _errandPartProvider;
        private readonly ErrandPartStore _errandPartStore;
        private readonly ErrandStatusProvider _errandStatus;

        public ErrandEditCommand(ErrandsEditViewModel viewModel, IServiceProvider databaseServices, CurrentUserStore currentUserStore, SelectedErrandStore selectedErrand)
        {
            _viewModel = viewModel;
            _databaseServices = databaseServices;
            _currentUser = currentUserStore;
            _currentErrand = selectedErrand;

            _errandProvider = _databaseServices.GetRequiredService<ErrandProvider>();
            _errandPartProvider = _databaseServices.GetRequiredService<ErrandPartProvider>();
            _errandStatus = _databaseServices.GetRequiredService<ErrandStatusProvider>();
            _errandPartStore = _databaseServices.GetRequiredService<ErrandPartStore>();
        }

        public override void Execute(object? parameter)
        {
            int errandId = _currentErrand.SelectedErrand?.Id ?? -1;
            if (HasErrors(errandId)) return;

            UpdateErrand(errandId);

            if (_currentErrand.ErrandParts.Count == 0)
            {
                _ = _errandStatus.Create(new ErrandStatus(
                    errandId,
                    ErrandStatusList.NoPartsList,
                    DateTime.Now
                ));
            }

            UpdateParts(errandId);

            _viewModel.ReloadData();
        }

        private bool HasErrors(int errandId)
        {
            if (!_viewModel.IsDataValidate) return true;
            if (!_viewModel.PartsList?.IsDataValidate ?? false) return true;
            if (errandId == -1) return true;
            return false;
        }

        private void UpdateErrand(int errandId)
        {
            ErrandDTO errandDTO = _viewModel.ErrandDTO;
            Errand errand = new(
                errandId,
                DateTime.Now,
                null,
                errandDTO.Machine?.Id,
                errandDTO.ErrandType?.Id,
                errandDTO.Description ?? "BRAK OPISU",
                errandDTO.SapNumber,
                errandDTO.ExpectedDate,
                errandDTO.Responsible?.Id,
                errandDTO.Priority);
            _ = _errandProvider.Update(errand);
        }

        private void UpdateParts(int errandId)
        {
            IEnumerable<ErrandPart> forCurrentErrand = _errandPartStore.Data.Where(ep => ep.ErrandId == errandId);
            IEnumerable<ErrandPart> existing = _currentErrand.ErrandParts.Intersect(forCurrentErrand);
            IEnumerable<ErrandPart> toDelete = forCurrentErrand.Except(_currentErrand.ErrandParts);
            IEnumerable<ErrandPart> toAdd = _currentErrand.ErrandParts.Except(forCurrentErrand);

            List<Task> tasks = [];
            if (existing.Any()) tasks.Add(Edit(existing));
            if (toDelete.Any()) tasks.Add(Remove(errandId, toDelete));
            if (toAdd.Any()) tasks.Add(Add(errandId, toAdd));
            Task.WhenAll(tasks);

            if (!forCurrentErrand.Any() && toAdd.Any())
            {
                ErrandStatus errandStatus = new(errandId, ErrandStatusList.PartsListCompleted, DateTime.Now);
                _ = _errandStatus.Create(errandStatus);
            }
        }

        private async Task Edit(IEnumerable<ErrandPart> existingParts)
        {
            foreach (ErrandPart errandPart in existingParts)
            {
                errandPart.Status = ErrandPart.PartStatuses[0];
                await _errandPartProvider.Update(errandPart);
                await _errandPartStore.Reload();
            }
        }
        private async Task Remove(int errandId, IEnumerable<ErrandPart> existingParts)
        {
            foreach (ErrandPart errandPart in existingParts)
            {
                //if (errandPart.PartId is null) continue;
                await _errandPartProvider.Delete(errandId, (int)errandPart.PartId);
            }
        }
        private async Task Add(int errandId, IEnumerable<ErrandPart> existingParts)
        {
            foreach (ErrandPart errandPart in existingParts)
            {
                await _errandPartProvider.Create(new ErrandPart(errandId, errandPart.PartId, errandPart.Amount));
            }
        }
    }
}