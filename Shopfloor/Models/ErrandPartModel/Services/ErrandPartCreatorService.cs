using System;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartStatusModel;

namespace Shopfloor.Models.ErrandPartModel.Services
{
    internal class ErrandPartCreatorService : IModelCreatorService<ErrandPart>
    {
        private readonly IDataModelDatabaseService<ErrandPart> _databaseService;
        private readonly IDataModelStoreService<ErrandPart> _storeService;
        private readonly IModelCreatorService<ErrandPartStatus> _statusCreator;
        public ErrandPartCreatorService(
            IDataModelDatabaseService<ErrandPart> databaseService,
            IDataModelStoreService<ErrandPart> storeService,
            IModelCreatorService<ErrandPartStatus> statusCreator)
        {
            _databaseService = databaseService;
            _storeService = storeService;
            _statusCreator = statusCreator;
        }
        public void Create(ErrandPart item)
        {
            int id = _databaseService.AddToDatabase(item);
            item.Id = id;
            _storeService.AddToStore(item);
            CreateErrandPartStatus(id);
        }
        private void CreateErrandPartStatus(int id)
        {
            ErrandPartStatus status = new(0)
            {
                ErrandPartId = id,
                CreatedDate = DateTime.Now
            };
            _statusCreator.Create(status);
        }
    }
}