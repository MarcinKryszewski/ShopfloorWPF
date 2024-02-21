using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartStatusModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel
{
    internal sealed class ErrandPartStore : IDataStore<ErrandPart>
    {
        private readonly IServiceProvider _databaseServices;
        private List<ErrandPart> _data = [];
        public ErrandPartStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public List<ErrandPart> Data => new(_data);
        public bool IsLoaded { get; private set; }
        public bool HasStatuses { get; private set; }
        public Task Load()
        {
            ErrandPartProvider provider = _databaseServices.GetRequiredService<ErrandPartProvider>();
            _data = new(provider.GetAll().Result);
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            ErrandPartProvider provider = _databaseServices.GetRequiredService<ErrandPartProvider>();
            _data = new(await provider.GetAll());
        }
        public async Task CombineData()
        {
            List<Task> tasks = [];

            if (!HasStatuses) tasks.Add(SetStatuses());

            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private Task SetStatuses()
        {
            List<ErrandPartStatus> statuses = _databaseServices.GetRequiredService<ErrandPartStatusStore>().Data;
            foreach (ErrandPart errandPart in _data)
            {
                errandPart.StatusList.Clear();
                errandPart.StatusList.AddRange(statuses.Where(status => status.ErrandPartId == errandPart.Id));
            }
            HasStatuses = true;
            return Task.CompletedTask;
        }
    }
}