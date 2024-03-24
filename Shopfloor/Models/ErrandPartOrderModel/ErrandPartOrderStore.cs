using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandPartOrderModel
{
    internal sealed class ErrandPartOrderStore : IDataStore<ErrandPartOrder>
    {
        private readonly IServiceProvider _databaseServices;
        private List<ErrandPartOrder> _data = [];
        public ErrandPartOrderStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public List<ErrandPartOrder> GetData => _data;
        public bool IsLoaded { get; private set; }
        public Task Load()
        {
            ErrandPartOrderProvider provider = _databaseServices.GetRequiredService<ErrandPartOrderProvider>();
            _data = new(provider.GetAll().Result);
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            ErrandPartOrderProvider provider = _databaseServices.GetRequiredService<ErrandPartOrderProvider>();
            _data = new(await provider.GetAll());
        }
        public async Task CombineData()
        {
            List<Task> tasks = [];



            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
    }
}