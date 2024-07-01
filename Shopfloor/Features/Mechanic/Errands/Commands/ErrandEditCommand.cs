using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartStatusModel;
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
        private readonly ErrandEditViewModel _viewModel;
        private readonly int _currentUserId;
        private readonly SelectedErrandStore _currentErrand;
        private readonly IProvider<Errand> _errandProvider;
        private readonly ErrandPartProvider _errandPartProvider;
        private readonly IDataStore<ErrandPart> _errandPartStore;
        private readonly ErrandStatusProvider _errandStatus;
        private readonly ErrandPartStatusProvider _errandPartStatusProvider;
        public ErrandEditCommand(ErrandEditViewModel viewModel, ICurrentUserStore currentUser, SelectedErrandStore selectedErrand, IProvider<Errand> errandProvider, ErrandPartProvider errandPartProvider, ErrandStatusProvider errandStatusProvider, IDataStore<ErrandPart> errandPartStore, ErrandPartStatusProvider errandPartStatusProvider)
        {
            _viewModel = viewModel;
            _currentUserId = currentUser.User?.Id ?? -1;
            _currentErrand = selectedErrand;

            _errandProvider = errandProvider;
            _errandPartProvider = errandPartProvider;
            _errandStatus = errandStatusProvider;
            _errandPartStore = errandPartStore;
            _errandPartStatusProvider = errandPartStatusProvider;
        }
        public override void Execute(object? parameter)
        {
            int errandId = _currentErrand.SelectedErrand?.Id ?? -1;
            if (HasErrors(errandId)) return;

            UpdateErrand(errandId);

            if (_currentErrand.ErrandParts.Count == 0)
            {
                _ = _errandStatus.Create(new ErrandStatus()
                {
                    ErrandId = errandId,
                    StatusName = ErrandStatusList.NoPartsList,
                    SetDate = DateTime.Now,
                });
            }

            UpdateParts(errandId);
            _viewModel.ReloadData();
        }
        private bool HasErrors(int errandId)
        {
            if (!_viewModel.IsDataValidate) return true;
            if (!_viewModel.PartsList?.IsDataValidate ?? false) return true;
            if (errandId == -1) return true;
            if (_currentUserId == -1) return true;
            return false;
        }
        private void UpdateErrand(int errandId)
        {
            ErrandDTO errandDTO = _viewModel.ErrandDTO;
            Errand errand = new()
            {
                Id = errandId,
                CreatedDate = DateTime.Now,
                Description = errandDTO.Description ?? "BRAK OPISU",
                Priority = errandDTO.Priority,
                MachineId = errandDTO.Machine?.Id,
                TypeId = errandDTO.ErrandType?.Id,
                SapNumber = errandDTO.SapNumber,
                ExpectedDate = errandDTO.ExpectedDate,
                OwnerId = errandDTO.Responsible?.Id,
                CreatedById = -1,
            };
            _ = _errandProvider.Update(errand);
        }
        private void UpdateParts(int errandId)
        {
            IEnumerable<ErrandPart> forCurrentErrand = _errandPartStore.Data.Where(ep => ep.ErrandId == errandId);
            IEnumerable<ErrandPart> existing = _currentErrand.ErrandParts.Intersect(forCurrentErrand);
            IEnumerable<ErrandPart> toDelete = forCurrentErrand.Except(_currentErrand.ErrandParts);
            IEnumerable<ErrandPart> toAdd = _currentErrand.ErrandParts.Except(forCurrentErrand);

            List<Task> tasks = [];
            if (existing.Any()) tasks.Add(EditParts(existing));
            if (toDelete.Any()) tasks.Add(RemoveParts(errandId, toDelete));
            if (toAdd.Any()) tasks.Add(AddParts(errandId, toAdd));
            Task.WhenAll(tasks);

            if (!forCurrentErrand.Any() && toAdd.Any())
            {
                ErrandStatus errandStatus = new()
                {
                    ErrandId = errandId,
                    StatusName = ErrandStatusList.PartsListCompleted,
                    SetDate = DateTime.Now,
                };
                _ = _errandStatus.Create(errandStatus);
            }
        }
        private async Task EditParts(IEnumerable<ErrandPart> existingParts)
        {
            foreach (ErrandPart errandPart in existingParts)
            {
                //errandPart.Status = ErrandPart.PartStatuses[0];
                await _errandPartProvider.Update(errandPart);
                SetNewErrandPartStatus(errandPart.PartId, ErrandPartStatus.Status[-7]);
                await _errandPartStore.Reload();
            }
        }
        private Task RemoveParts(int errandId, IEnumerable<ErrandPart> existingParts)
        {
            foreach (ErrandPart errandPart in existingParts)
            {
                //if (errandPart.PartId is null) continue;
                //await _errandPartProvider.Delete(errandId, errandPart.PartId);
                SetNewErrandPartStatus(errandPart.PartId, ErrandPartStatus.Status[-6]);
            }
            return Task.CompletedTask;
        }
        private async Task AddParts(int errandId, IEnumerable<ErrandPart> existingParts)
        {
            foreach (ErrandPart errandPart in existingParts)
            {
                int errandPartId = await _errandPartProvider.Create(new ErrandPart() { ErrandId = errandId, PartId = errandPart.PartId, Amount = errandPart.Amount, OrderedById = _currentUserId });
                SetNewErrandPartStatus(errandPartId, ErrandPartStatus.Status[0]);
            }
        }
        private void SetNewErrandPartStatus(int errandPartId, string statusName)
        {
            ErrandPartStatus partStatus = new(statusName)
            {
                ErrandPartId = errandPartId,
                CreatedDate = DateTime.Now,
                CompletedById = null,
            };
            _ = _errandPartStatusProvider.Create(partStatus);
        }
    }
}