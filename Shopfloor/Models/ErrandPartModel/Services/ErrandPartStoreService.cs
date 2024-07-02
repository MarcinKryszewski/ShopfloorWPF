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
        public void AddToStore(ErrandPart item) => _store.Data.Add(item);

        public void EditInStore(ErrandPart item)
        {
            throw new System.NotImplementedException();
        }

        public ErrandPart? FindItem(ErrandPart item)
        {
            return _store.Data.FirstOrDefault(t => t.Id == item.Id);
        }
    }
}