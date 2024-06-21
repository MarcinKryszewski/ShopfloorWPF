namespace Shopfloor.Models.ErrandStatusModel.Services
{
    internal class ErrandStatusStoreService
    {
        private readonly ErrandStatusStore _errandStatusStore;
        public ErrandStatusStoreService(ErrandStatusStore errandStatusStore)
        {
            _errandStatusStore = errandStatusStore;
        }
        public void AddErrandStatusToStore(ErrandStatus status) => _errandStatusStore.Data.Add(status);
    }
}