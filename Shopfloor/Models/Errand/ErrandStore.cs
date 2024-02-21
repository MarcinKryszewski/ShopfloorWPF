using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Shared;

namespace Shopfloor.Models.ErrandModel
{
    internal sealed class ErrandStore : IDataStore<Errand>
    {
        private readonly IServiceProvider _databaseServices;
        private List<Errand> _data = [];
        public ErrandStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public List<Errand> Data => _data;
        public bool IsLoaded { get; private set; }
        public bool HasParts { get; private set; }
        public bool HasStatuses { get; private set; }
        public bool HasUsers { get; private set; }
        public bool HasMachines { get; private set; }
        public bool HasTypes { get; private set; }
        public Task Load()
        {
            ErrandProvider provider = _databaseServices.GetRequiredService<ErrandProvider>();
            try
            {
                _data = new(provider.GetAll().Result);
                IsLoaded = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            ErrandProvider provider = _databaseServices.GetRequiredService<ErrandProvider>();
            _data = new(await provider.GetAll());
        }
        public async Task CombineData()
        {
            List<Task> tasks = [];

            if (!HasParts) tasks.Add(SetParts());
            if (!HasStatuses) tasks.Add(SetStatuses());
            if (!HasUsers) tasks.Add(SetUsers());
            if (!HasMachines) tasks.Add(SetMachines());
            if (!HasTypes) tasks.Add(SetTypes());

            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private async Task SetParts()
        {
            List<ErrandPart> errandParts = await DataStore.GetData(_databaseServices.GetRequiredService<ErrandPartStore>());
            foreach (Errand errand in _data)
            {
                errand.Parts.Clear();
                errand.Parts.AddRange(errandParts.Where(errandPart => errandPart.ErrandId == errand.Id));
            }
            HasParts = true;
        }
        private async Task SetStatuses()
        {
            List<ErrandStatus> errandStatuses = await DataStore.GetData(_databaseServices.GetRequiredService<ErrandStatusStore>());
            foreach (Errand errand in _data)
            {
                errand.Statuses.Clear();
                errand.Statuses.AddRange(errandStatuses.Where(errandStatus => errandStatus.ErrandId == errand.Id));
            }
            HasStatuses = true;
        }
        private async Task SetUsers()
        {
            List<User> users = await DataStore.GetData(_databaseServices.GetRequiredService<UserStore>());
            foreach (Errand errand in _data)
            {
                errand.CreatedByUser = users.FirstOrDefault(user => user.Id == errand.CreatedById);
                errand.Responsible = users.FirstOrDefault(user => user.Id == errand.OwnerId);
            }
            HasUsers = true;
        }
        private async Task SetMachines()
        {
            List<Machine> machines = await DataStore.GetData(_databaseServices.GetRequiredService<MachineStore>());
            foreach (Errand errand in _data)
            {
                errand.Machine = machines.FirstOrDefault(machine => machine.Id == errand.MachineId);
            }
            HasMachines = true;
        }
        private async Task SetTypes()
        {
            List<ErrandType> types = await DataStore.GetData(_databaseServices.GetRequiredService<ErrandTypeStore>());
            foreach (Errand errand in _data)
            {
                errand.Type = types.FirstOrDefault(type => type.Id == errand.TypeId);
            }
            HasTypes = true;
        }
        /*private static async Task<List<T>> LoadData<T>(IDataStore<T> dataStore)
        {
            if (!dataStore.IsLoaded) await dataStore.Load();
            return dataStore.Data;
        }*/
    }
}