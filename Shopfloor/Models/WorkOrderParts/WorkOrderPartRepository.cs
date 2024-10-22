using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.WorkOrderParts
{
    internal class WorkOrderPartRepository : IRepository<WorkOrderPartModel, WorkOrderPartCreationModel>
    {
        private readonly IStore<WorkOrderPartModel> _store;
        private readonly IProvider<WorkOrderPartModel, WorkOrderPartCreationModel> _provider;
        private bool _dataLoaded = false;
        public WorkOrderPartRepository(
            IStore<WorkOrderPartModel> store,
            IProvider<WorkOrderPartModel, WorkOrderPartCreationModel> provider)
        {
            _store = store;
            _provider = provider;
        }
        public HashSet<Type> Merges { get; } = [];
        public async Task<WorkOrderPartModel> Create(WorkOrderPartCreationModel item)
        {
            int id = await _provider.Create(item);
            WorkOrderPartModel model = item.CreateModel(id);
            await _store.AddItem(model);
            return model;
        }
        public async Task Delete(int id)
        {
            WorkOrderPartModel? item = _store.Data.Find(x => x.Id == id);
            if (item == null)
            {
                string errorText = "Nie udało się anulować tego działania. Spróbuj ponownie!";
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }
            try
            {
                await _provider.Delete(id);
                _store.Data.Remove(item);
            }
            catch (Exception)
            {
                string errorText = "Nie udało się anulować tego działania. Spróbuj ponownie!";
                await Task.FromException(new InvalidOperationException(errorText));
            }
        }
        public async Task<List<WorkOrderPartModel>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                List<WorkOrderPartModel> data = (await _provider.GetAll()).ToList();
                _store.Data.AddRange(data);
                _dataLoaded = true;
            }

            await Task.Delay(0);
            return _store.Data;
        }
        public async Task Update(WorkOrderPartCreationModel item)
        {
            WorkOrderPartModel? existingData = _store.Data.Find(x => x.Id == item.Id);

            if (existingData is null)
            {
                string errorText = "Nie udało się zaktualizować tej części. Spróbuj ponownie!";
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }

            await _provider.Update(existingData);
            existingData.SetValues(item);

            await Task.CompletedTask;
        }
    }
}