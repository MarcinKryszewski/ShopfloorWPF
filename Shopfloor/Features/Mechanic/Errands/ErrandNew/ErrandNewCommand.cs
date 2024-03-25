using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Stores;
using System;
using System.Threading.Tasks;
using Shopfloor.Models.ErrandPartModel.Store;

namespace Shopfloor.Features.Mechanic.Errands.ErrandNew
{
    internal sealed partial class ErrandNewCommand : CommandBase
    {
        private readonly ErrandNewViewModel _viewModel;
        private readonly ErrandProvider _errandProvider;
        private readonly int _currentUserId;
        private readonly SelectedErrandStore _currentErrand;
        private readonly ErrandPartProvider _errandPartProvider;
        private readonly ErrandPartStatusProvider _errandPartStatusProvider;
        private readonly ErrandStatusProvider _errandStatusProvider;
        private bool _isPartAdd;
        public ErrandNewCommand(ErrandNewViewModel errandsNewViewModel, CurrentUserStore currentUser, SelectedErrandStore currentErrand)
        {
            _viewModel = errandsNewViewModel;
            _currentUserId = currentUser.User?.Id ?? -1;
            _currentErrand = currentErrand;

            //_errandProvider = _databaseServices.GetRequiredService<ErrandProvider>();
            //_errandPartProvider = _databaseServices.GetRequiredService<ErrandPartProvider>();
            //_errandStatusProvider = _databaseServices.GetRequiredService<ErrandStatusProvider>();
            //_errandPartStatusProvider = _databaseServices.GetRequiredService<ErrandPartStatusProvider>();
        }
        public required ErrandProvider ErrandProvider
        {
            init => _errandProvider = value;
        }
        public required ErrandPartProvider ErrandPartProvider
        {
            init => _errandPartProvider = value;
        }
        public required ErrandStatusProvider ErrandStatusProvider
        {
            init => _errandStatusProvider = value;
        }
        public required ErrandPartStatusProvider ErrandPartStatusProvider
        {
            init => _errandPartStatusProvider = value;
        }
        public override void Execute(object? parameter)
        {
            _isPartAdd = false;
            if (HasErrors()) return;
            AddErrand();
            ResetForm();
        }
        private void ResetForm()
        {
            _viewModel.CleanForm();
        }
        private bool HasErrors()
        {
            if (!_viewModel.IsDataValidate) return true;
            if (!_viewModel.PartsList?.IsDataValidate ?? false) return true;
            if (_currentUserId == -1) return true;
            return false;
        }
        private void AddParts(int errandId)
        {
            foreach (ErrandPart errandPart in _currentErrand.ErrandParts)
            {
                int errandPartId = _errandPartProvider.Create(new ErrandPart() { ErrandId = errandId, PartId = errandPart.PartId, Amount = errandPart.Amount, OrderedById = _currentUserId }).Result;
                SetNewErrandPartStatus(errandPartId);
                _isPartAdd = true;
            }
            Task.Run(_errandPartStore.Reload);
            Task.Run(_errandPartStatusStore.Reload);
        }
        private void AddErrand()
        {
            _currentErrand.ErrandId = CreateErrand();
            Task.Run(_errandStore.Reload);
            SetErrandStatus((int)_currentErrand.ErrandId, ErrandStatusList.NoPartsList);
            AddParts((int)_currentErrand.ErrandId);
            if (_isPartAdd) SetErrandStatus((int)_currentErrand.ErrandId, ErrandStatusList.PartsListCompleted);
        }
        private int CreateErrand()
        {
            ErrandDTO errandDTO = _viewModel.ErrandDTO;

            Errand errand = new()
            {
                ExpectedDate = errandDTO.ExpectedDate,
                Responsible = errandDTO.Responsible,
                SapNumber = errandDTO.SapNumber,
                CreatedById = _currentUserId,
                CreatedDate = DateTime.Now,
                Description = errandDTO.Description ?? "BRAK OPISU",
                Priority = errandDTO.Priority,
                MachineId = errandDTO.Machine?.Id,
                TypeId = errandDTO.ErrandType?.Id,
            };

            return _errandProvider.Create(errand).Result;
        }
        private void SetNewErrandPartStatus(int errandPartId)
        {
            ErrandPartStatus partStatus = new(ErrandPartStatus.Status[0])
            {
                ErrandPartId = errandPartId,
                CreatedDate = DateTime.Now
            };
            _ = _errandPartStatusProvider.Create(partStatus);
        }
        private void SetErrandStatus(int errandId, string statusName)
        {
            if (_isPartAdd)
            {
                ErrandStatus errandStatus = new()
                {
                    ErrandId = errandId,
                    StatusName = statusName,
                    SetDate = DateTime.Now,
                };
                _ = _errandStatusProvider.Create(errandStatus);
                _errandStatusStore.Reload().Start();
            }
        }
        private ErrandStatusStore _errandStatusStore;
        public required ErrandStatusStore ErrandStatusStore
        {
            private get => _errandStatusStore;
            init => _errandStatusStore = value;
        }
        private ErrandStore _errandStore;
        public required ErrandStore ErrandStore
        {
            private get => _errandStore;
            init => _errandStore = value;
        }
        private ErrandPartStore _errandPartStore;
        public required ErrandPartStore ErrandPartStore
        {
            private get => _errandPartStore;
            init => _errandPartStore = value;
        }
        private ErrandPartStatusStore _errandPartStatusStore;
        public required ErrandPartStatusStore ErrandPartStatusStore
        {
            private get => _errandPartStatusStore;
            init => _errandPartStatusStore = value;
        }
    }
}