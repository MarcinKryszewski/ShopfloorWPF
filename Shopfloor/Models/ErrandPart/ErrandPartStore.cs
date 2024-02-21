using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Shared;
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
        public bool HasParts { get; private set; }
        public bool HasUsers { get; private set; }
        public bool HasErrands { get; private set; }

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
            if (!HasParts) tasks.Add(SetParts());
            if (!HasUsers) tasks.Add(SetUsers());
            if (!HasErrands) tasks.Add(SetErrands());

            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private async Task SetStatuses()
        {
            List<ErrandPartStatus> statuses = await DataStore.GetData(_databaseServices.GetRequiredService<ErrandPartStatusStore>());
            foreach (ErrandPart errandPart in _data)
            {
                errandPart.StatusList.Clear();
                errandPart.StatusList.AddRange(statuses.Where(status => status.ErrandPartId == errandPart.Id));
            }
            HasStatuses = true;
        }
        private async Task SetParts()
        {
            List<Part> parts = await DataStore.GetData(_databaseServices.GetRequiredService<PartsStore>());
            foreach (ErrandPart errandPart in _data)
            {
                errandPart.Part = parts.FirstOrDefault(part => part.Id == errandPart.PartId);
            }
            HasParts = true;
        }
        private async Task SetUsers()
        {
            List<User> users = await DataStore.GetData(_databaseServices.GetRequiredService<UserStore>());
            foreach (ErrandPart errandPart in _data)
            {
                errandPart.OrderedByUser = users.FirstOrDefault(user => user.Id == errandPart.OrderedById);
            }
            HasUsers = true;
        }
        private async Task SetErrands()
        {
            List<Errand> errands = await DataStore.GetData(_databaseServices.GetRequiredService<ErrandStore>());
            foreach (ErrandPart errandPart in _data)
            {
                errandPart.Errand = errands.FirstOrDefault(errand => errand.Id == errandPart.ErrandId);
            }
            HasErrands = true;
        }
    }
}