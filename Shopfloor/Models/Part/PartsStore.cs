using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Models.PartModel
{
    internal sealed class PartsStore : IDataStore<Part>
    {
        private List<Part> _data = [];
        private readonly IServiceProvider _databaseServices;

        public List<Part> Data => _data;
        public bool IsLoaded { get; private set; }
        public bool HasTypes { get; private set; }

        public PartsStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }

        public Task Load()
        {
            PartProvider provider = _databaseServices.GetRequiredService<PartProvider>();
            _data = new(provider.GetAll().Result);
            IsLoaded = true;
            return Task.CompletedTask;
        }

        public Task Reload()
        {
            throw new NotImplementedException();
        }

        public async Task CombineData()
        {
            List<Task> tasks = [];

            if (!HasTypes) tasks.Add(SetTypes());

            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
        private async Task SetTypes()
        {
            List<PartType> types = await DataStore.GetData(_databaseServices.GetRequiredService<PartTypesStore>());
            foreach (Part part in _data)
            {
                part.SetType(types.FirstOrDefault(type => type.Id == part.TypeId));
            }
            HasTypes = true;
        }
    }
}