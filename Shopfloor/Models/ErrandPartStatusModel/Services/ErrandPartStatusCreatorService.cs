using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;

namespace Shopfloor.Models.ErrandPartStatusModel.Services
{
    internal class ErrandPartStatusCreatorService : IModelCreatorService<ErrandPartStatus>
    {
        private readonly IDataModelDatabaseService<ErrandPartStatus> _databaseService;
        private readonly IDataModelStoreService<ErrandPartStatus> _storeService;
        public ErrandPartStatusCreatorService(
            IDataModelStoreService<ErrandPartStatus> storeService,
            IDataModelDatabaseService<ErrandPartStatus> databaseService)
        {
            _storeService = storeService;
            _databaseService = databaseService;
        }
        public void Create(ErrandPartStatus item)
        {
            int id = _databaseService.Add(item);
            item.Id = id;
            _storeService.Add(item);
        }
    }
}