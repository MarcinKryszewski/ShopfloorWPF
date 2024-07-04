using System.Linq;
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;

namespace Shopfloor.Models.ErrandPartModel.Services
{
    internal class ErrandPartStoreService : IDataModelStoreService<ErrandPart>
    {
        private readonly IDataStore<ErrandPart> _store;
        public ErrandPartStoreService(IDataStore<ErrandPart> store)
        {
            _store = store;
        }
        public void Add(ErrandPart item) => _store.Data.Add(item);
        public void Edit(ErrandPart item)
        {
            int position = FindPosition(item);
            if (position != -1) _store.Data[position] = item;
        }
        public void Remove(ErrandPart item)
        {
            int position = FindPosition(item);
            _store.Data.RemoveAt(position);
        }
        private int FindPosition(ErrandPart item) => _store.Data.FindIndex(t => t.Id == item.Id);
        public ErrandPart? FindItem(ErrandPart item) => _store.Data.FirstOrDefault(t => t.Id == item.Id);
    }
}