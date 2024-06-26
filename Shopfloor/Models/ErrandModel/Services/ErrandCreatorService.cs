using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandStatusModel;
using System;

namespace Shopfloor.Models.ErrandModel.Services
{
    internal class ErrandCreatorService : IModelCreatorService<Errand>
    {
        private readonly IDataModelDatabaseService<Errand> _databaseService;
        private readonly IDataModelStoreService<Errand> _storeService;
        private readonly IModelCreatorService<ErrandStatus> _statusCreator;
        public ErrandCreatorService(
            IDataModelDatabaseService<Errand> errandDatabaseService,
            IDataModelStoreService<Errand> errandStoreService,
            IModelCreatorService<ErrandStatus> statusCreator)
        {
            _databaseService = errandDatabaseService;
            _storeService = errandStoreService;
            _statusCreator = statusCreator;
        }
        public void Create(Errand item)
        {
            int errandId = _databaseService.AddToDatabase(item);
            item.Id = errandId;
            _storeService.AddToStore(item);
            CreateErrandStatus(errandId);
        }

        private void CreateErrandStatus(int errandId)
        {
            string defaultReason = "NEW ERRAND CREATED";
            ErrandStatus status = new()
            {
                ErrandId = errandId,
                StatusName = ErrandStatusList.NoPartsList,
                SetDate = DateTime.Now,
                Reason = defaultReason
            };
            _statusCreator.Create(status);
        }
    }
}