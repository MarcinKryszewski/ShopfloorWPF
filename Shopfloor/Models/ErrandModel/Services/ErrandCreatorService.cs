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
            CreateErrandStatus(item);
        }

        private void CreateErrandStatus(Errand errand)
        {
            string defaultReason = "NEW ERRAND CREATED";
            if (errand.Id is null) return;

            ErrandStatus status = new()
            {
                ErrandId = (int)errand.Id,
                StatusName = ErrandStatusList.NoPartsList,
                SetDate = DateTime.Now,
                Reason = defaultReason
            };
            errand.AddStatus(status);
            _statusCreator.Create(status);
        }
    }
}