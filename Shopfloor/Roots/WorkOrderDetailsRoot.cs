using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Contexts;
using Shopfloor.Contexts.PartsBasket;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Parts;
using Shopfloor.Models.WorkOrderParts;

namespace Shopfloor.Roots
{
    internal class WorkOrderDetailsRoot : IRoot
    {
        private readonly PartsBasketContext _partsBasket;
        private readonly IRepository<WorkOrderPartModel, WorkOrderPartCreationModel> _workOrderPartRepository;
        private readonly WorkOrderContext _store;
        private readonly IRepository<PartModel, PartCreationModel> _partRepository;

        public WorkOrderDetailsRoot(
            IRepository<PartModel, PartCreationModel> partRepository,
            PartsBasketContext partsBasket,
            IRepository<WorkOrderPartModel, WorkOrderPartCreationModel> workOrderPartRepository,
            WorkOrderContext store)
        {
            _partsBasket = partsBasket;
            _workOrderPartRepository = workOrderPartRepository;
            _store = store;
            _partRepository = partRepository;
        }
        public event EventHandler? DataChanged;
        public List<WorkOrderPartModel> Parts { get; } = [];
        public Task LoadBasket()
        {
            if (_store.WorkOrder is null)
            {
                return Task.CompletedTask;
            }

            List<WorkOrderPartModel>? parts = new(_workOrderPartRepository
                .GetDataAsync()
                .Result
                .Where(x => x.WorkOrderId == _store.WorkOrder.Id));

            foreach (var item in parts)
            {
                WorkOrderPartCreationModel creationModel = new();
                _partsBasket.Parts.Add(creationModel.CreateFromModel(item));
            }
            if (!_workOrderPartRepository.Merges.Contains(typeof(PartModel)))
            {
                _ = DecorateBasketWithParts();
            }
            return Task.CompletedTask;
        }
        protected void OnDataChanged(EventArgs e) => DataChanged?.Invoke(this, e);
        private async Task DecorateBasketWithParts()
        {
            List<PartModel>? parts = await _partRepository.GetDataAsync();

            foreach (WorkOrderPartCreationModel item in _partsBasket.Parts)
            {
                item.Part = parts.Find(part => part.Id == item.PartId);
            }

            OnDataChanged(EventArgs.Empty);
        }
    }
}