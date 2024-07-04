using System;
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
using Shopfloor.Models.ErrandPartStatusModel;

namespace Shopfloor.Models.ErrandPartModel.Services
{
    internal class ErrandPartDeleterService : IModelDeleterService<ErrandPart>
    {
        private readonly IDataModelDatabaseService<ErrandPart> _databaseService;
        private readonly IDataModelStoreService<ErrandPart> _storeService;
        private readonly IModelCreatorService<ErrandPartStatus> _statusCreator;
        public ErrandPartDeleterService(
            IModelCreatorService<ErrandPartStatus> statusCreator,
            IDataModelStoreService<ErrandPart> storeService,
            IDataModelDatabaseService<ErrandPart> databaseService)
        {
            _statusCreator = statusCreator;
            _storeService = storeService;
            _databaseService = databaseService;
        }
        public void Delete(ErrandPart item)
        {
            _databaseService.Delete(item);
            RemoveFromStore(item);
        }

        private void RemoveFromStore(ErrandPart item)
        {
            // _storeService.
        }
    }
}