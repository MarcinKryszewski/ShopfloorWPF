using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandPartOfferModel
{
    internal sealed class ErrandPartOfferStore : IDataStore<ErrandPartOffer>
    {
        private readonly IServiceProvider _databaseServices;
        private List<ErrandPartOffer> _data = [];
        public ErrandPartOfferStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public List<ErrandPartOffer> Data => _data;
        public bool IsLoaded { get; private set; }
        public Task Load()
        {
            ErrandPartOfferProvider provider = _databaseServices.GetRequiredService<ErrandPartOfferProvider>();
            _data = new(provider.GetAll().Result);
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            ErrandPartOfferProvider provider = _databaseServices.GetRequiredService<ErrandPartOfferProvider>();
            _data = new(await provider.GetAll());
        }
        public async Task CombineData()
        {
            List<Task> tasks = [];



            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
    }
}