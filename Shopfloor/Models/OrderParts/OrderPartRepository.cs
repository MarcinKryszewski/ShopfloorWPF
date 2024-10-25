using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.OrderParts
{
    internal class OrderPartRepository : IRepository<OrderPartModel, OrderPartCreationModel>
    {
        private readonly IStore<OrderPartModel> _store;
        private readonly IProvider<OrderPartModel, OrderPartCreationModel> _provider;
        private bool _dataLoaded = false;
        public OrderPartRepository(
            IStore<OrderPartModel> store,
            IProvider<OrderPartModel, OrderPartCreationModel> provider)
        {
            _store = store;
            _provider = provider;
        }
        public HashSet<Type> Merges { get; } = [];
        public async Task<OrderPartModel> Create(OrderPartCreationModel item)
        {
            int id = await _provider.Create(item);
            OrderPartModel order = item.CreateModel(id);
            _store.Data.Add(order);
            return order;
        }
        public async Task Delete(int id)
        {
            OrderPartModel? item = _store.Data.Find(x => x.Id == id);
            if (item == null)
            {
                string errorText = "Nie udało się anulować tego zamówienia. Spróbuj ponownie!";
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
                string errorText = "Nie udało się anulować tego zamówienia. Spróbuj ponownie!";
                await Task.FromException(new InvalidOperationException(errorText));
            }
        }
        public async Task<List<OrderPartModel>> GetDataAsync()
        {
            if (!_dataLoaded)
            {
                List<OrderPartModel> data = (await _provider.GetAll()).ToList();
                _store.Data.AddRange(data);
                _dataLoaded = true;
            }

            return _store.Data;
        }
        public async Task Update(OrderPartCreationModel item)
        {
            OrderPartModel? existingData = _store.Data.Find(x => x.Id == item.Id);

            if (existingData is null)
            {
                string errorText = "Nie udało się zaktualizować tego zamówienia. Spróbuj ponownie!";
                await Task.FromException(new InvalidOperationException(errorText));
                return;
            }

            await _provider.Update(existingData);
            existingData.SetValues(item);

            await Task.CompletedTask;
        }
    }
}