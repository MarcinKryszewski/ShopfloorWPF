using Shopfloor.Interfaces;

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
    }
}