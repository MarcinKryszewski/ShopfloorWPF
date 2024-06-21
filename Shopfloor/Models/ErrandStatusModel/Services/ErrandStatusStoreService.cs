namespace Shopfloor.Models.ErrandStatusModel.Services
{
    internal class ErrandStatusStoreService : IErrandStatusStoreService
    {
        private readonly ErrandStatusStore _errandStatusStore;
        public ErrandStatusStoreService(ErrandStatusStore errandStatusStore)
        {
            _errandStatusStore = errandStatusStore;
        }
        public void AddErrandStatusToStore(ErrandStatus status) => _errandStatusStore.Data.Add(status);
    }

    internal interface IErrandStatusStoreService
    {
        public void AddErrandStatusToStore(ErrandStatus item);
    }
}