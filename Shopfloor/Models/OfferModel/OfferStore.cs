using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.OfferModel
{
    internal sealed class OfferStore : IDataStore<Offer>
    {
        private readonly IServiceProvider _databaseServices;
        private List<Offer> _data = [];
        public OfferStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public List<Offer> GetData => _data;
        public bool IsLoaded { get; private set; }
        public Task Load()
        {
            OfferProvider provider = _databaseServices.GetRequiredService<OfferProvider>();
            _data = new(provider.GetAll().Result);
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            OfferProvider provider = _databaseServices.GetRequiredService<OfferProvider>();
            _data = new(await provider.GetAll());
        }
        public async Task CombineData()
        {
            List<Task> tasks = [];



            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
    }
}