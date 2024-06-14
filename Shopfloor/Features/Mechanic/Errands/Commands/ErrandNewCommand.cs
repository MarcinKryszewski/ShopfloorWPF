using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands.ErrandsNew;
using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Shared.Commands;
using Shopfloor.Stores;
using System;
using System.Threading.Tasks;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed partial class ErrandNewCommand : CommandBase
    {
        private readonly ErrandsNewViewModel _viewModel;
        private readonly IServiceProvider _databaseServices;
        private readonly ErrandProvider _errandProvider;
        private readonly int _currentUserId;
        private readonly SelectedErrandStore _currentErrand;
        private readonly ErrandPartProvider _errandPartProvider;
        private readonly ErrandPartStatusProvider _errandPartStatusProvider;
        private readonly ErrandStatusProvider _errandStatusProvider;
        private bool _isPartAdd;
        public ErrandNewCommand(ErrandsNewViewModel errandsNewViewModel, IServiceProvider databaseServices, CurrentUserStore currentUser, SelectedErrandStore currentErrand)
        {
            _viewModel = errandsNewViewModel;
            _databaseServices = databaseServices;
            _currentUserId = currentUser.User?.Id ?? -1;
            _currentErrand = currentErrand;

            _errandProvider = _databaseServices.GetRequiredService<ErrandProvider>();
            _errandPartProvider = _databaseServices.GetRequiredService<ErrandPartProvider>();
            _errandStatusProvider = _databaseServices.GetRequiredService<ErrandStatusProvider>();
            _errandPartStatusProvider = _databaseServices.GetRequiredService<ErrandPartStatusProvider>();
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
            _viewModel.ReloadData();
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
            Task.Run(_databaseServices.GetRequiredService<ErrandPartStore>().Reload);
            Task.Run(_databaseServices.GetRequiredService<ErrandPartStatusStore>().Reload);
        }
        private void AddErrand()
        {
            _currentErrand.ErrandId = CreateErrand();
            Task.Run(_databaseServices.GetRequiredService<ErrandStore>().Reload);
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
                Task.Run(_databaseServices.GetRequiredService<ErrandStatusStore>().Reload);
            }
        }
    }
}