using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandStatusModel.Services
{
    internal class ErrandStatusCreatorService : IModelCreatorService<ErrandStatus>
    {
        private readonly IDataModelDatabaseService<ErrandStatus> _databaseService;
        private readonly IDataModelStoreService<ErrandStatus> _storeService;
        public ErrandStatusCreatorService(
            IDataModelStoreService<ErrandStatus> storeService,
            IDataModelDatabaseService<ErrandStatus> databaseService)
        {
            _storeService = storeService;
            _databaseService = databaseService;
        }
        public void Create(ErrandStatus item)
        {
            int statusId = _databaseService.AddToDatabase(item);
            item.Id = statusId;
            _storeService.AddToStore(item);
        }
    }
}