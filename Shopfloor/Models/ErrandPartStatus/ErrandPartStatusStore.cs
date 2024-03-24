using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartStatusModel
{
    internal sealed class ErrandPartStatusStore : IDataStore<ErrandPartStatus>
    {
        private readonly IServiceProvider _databaseServices;
        private List<ErrandPartStatus> _data = [];
        public ErrandPartStatusStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public List<ErrandPartStatus> GetData(bool shouldCombine = false)
        {
            if (!IsLoaded) Load();
            if (shouldCombine) _combiner.Combine().Wait();
            return _data;
        }
        public bool IsLoaded { get; private set; }

        public Task CombineData()
        {
            throw new NotImplementedException();
        }

        public Task Load()
        {
            ErrandPartStatusProvider provider = _databaseServices.GetRequiredService<ErrandPartStatusProvider>();
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
            ErrandPartStatusProvider provider = _databaseServices.GetRequiredService<ErrandPartStatusProvider>();
            _data = new(await provider.GetAll());
        }
    }
}