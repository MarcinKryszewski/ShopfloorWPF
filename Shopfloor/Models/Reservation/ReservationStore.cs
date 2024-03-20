using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ReservationModel
{
    internal sealed class ReservationStore : IDataStore<Reservation>
    {
        private readonly IServiceProvider _databaseServices;
        private List<Reservation> _data = [];
        public ReservationStore(IServiceProvider databaseServices)
        {
            _databaseServices = databaseServices;
        }
        public List<Reservation> Data => _data;
        public bool IsLoaded { get; private set; }
        public Task Load()
        {
            ReservationProvider provider = _databaseServices.GetRequiredService<ReservationProvider>();
            _data = new(provider.GetAll().Result);
            IsLoaded = true;
            return Task.CompletedTask;
        }
        public async Task Reload()
        {
            ReservationProvider provider = _databaseServices.GetRequiredService<ReservationProvider>();
            _data = new(await provider.GetAll());
        }
        public async Task CombineData()
        {
            List<Task> tasks = [];



            if (tasks.Count > 0) await Task.WhenAll(tasks);
        }
    }
}