using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Contexts;
using Shopfloor.Contexts.PartsBasket;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Parts;
using Shopfloor.Models.WorkOrderParts;
using Shopfloor.Models.WorkOrders;

namespace Shopfloor.Roots
{
    internal class WorkOrderEditRoot : IRoot
    {
        private readonly PartsBasketContext _partsBasket;
        private readonly IRepository<WorkOrderPartModel, WorkOrderPartCreationModel> _workOrderPartRepository;
        private readonly IRepository<PartModel, PartCreationModel> _partRepository;
        private readonly WorkOrderContext _store;
        private readonly IRepository<LineModel, LineCreationModel> _lineRepository;
        private readonly IRepository<WorkOrderModel, WorkOrderCreationModel> _workOrderRepository;
        private readonly WorkOrderValidation _validation = new();
        public WorkOrderEditRoot(
            WorkOrderContext store,
            IRepository<PartModel, PartCreationModel> partRepository,
            PartsBasketContext partsBasket,
            IRepository<WorkOrderPartModel, WorkOrderPartCreationModel> workOrderPartRepository,
            IRepository<LineModel, LineCreationModel> lineRepository,
            IRepository<WorkOrderModel, WorkOrderCreationModel> workOrderRepository)
        {
            _lineRepository = lineRepository;
            _workOrderRepository = workOrderRepository;
            _partsBasket = partsBasket;
            _workOrderPartRepository = workOrderPartRepository;
            _store = store;
            _partRepository = partRepository;
        }
        public event EventHandler? DataChanged;
        public async Task EditWorkOrder(WorkOrderCreationModel data)
        {
            string errorText = "Nie udało się zmienić działania. Popraw dane i spróbuj ponownie!";
            _validation.Validate(data);
            if (data.HasErrors)
            {
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }
            try
            {
                await _workOrderRepository.Update(data);
                await WorkOrderPartsHandler();
            }
            catch (Exception)
            {
                await Task.FromException(new InvalidOperationException(errorText));
            }
        }
        public async Task<IEnumerable<LineModel>> GetLines() => await _lineRepository.GetDataAsync();
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
        private async Task WorkOrderPartsHandler()
        {
            List<Task> tasks = [];

            tasks.Add(UpdateParts());
            tasks.Add(AddParts());
            tasks.Add(DeleteParts());

            await Task.WhenAll(tasks);
        }
        private async Task UpdateParts()
        {
            List<Task> tasks = [];

            List<WorkOrderPartCreationModel> parts = _partsBasket.Parts.Intersect(_partsBasket.OriginalPartsList).ToList();
            foreach (WorkOrderPartCreationModel part in parts)
            {
                tasks.Add(_workOrderPartRepository.Update(part));
            }
            await Task.WhenAll(tasks);
        }
        private async Task AddParts()
        {
            List<Task> tasks = [];

            List<WorkOrderPartCreationModel> parts = _partsBasket.Parts.Except(_partsBasket.OriginalPartsList).ToList();
            foreach (WorkOrderPartCreationModel part in parts)
            {
                tasks.Add(_workOrderPartRepository.Update(part));
            }
            await Task.WhenAll(tasks);
        }
        private async Task DeleteParts()
        {
            List<Task> tasks = [];

            List<WorkOrderPartCreationModel> parts = _partsBasket.OriginalPartsList.Except(_partsBasket.Parts).ToList();
            foreach (WorkOrderPartCreationModel part in parts)
            {
                tasks.Add(_workOrderPartRepository.Update(part));
            }
            await Task.WhenAll(tasks);
        }
    }
}