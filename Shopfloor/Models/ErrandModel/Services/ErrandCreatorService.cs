using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandStatusModel.Services;
using System;

namespace Shopfloor.Models.ErrandModel.Services
{
    internal class ErrandCreatorService
    {
        private readonly ErrandDatabaseService _databaseService;
        private readonly ErrandStoreService _storeService;
        private readonly ErrandStatusDatabaseService _statusDatabaseService;
        private readonly ErrandStatusStoreService _statusStoreService;
        public ErrandCreatorService(
            ErrandDatabaseService errandDatabaseService,
            ErrandStoreService errandStoreService,
            ErrandStatusDatabaseService errandStatusDatabaseService,
            ErrandStatusStoreService errandStatusStoreService)
        {
            _databaseService = errandDatabaseService;
            _storeService = errandStoreService;
            _statusDatabaseService = errandStatusDatabaseService;
            _statusStoreService = errandStatusStoreService;
        }
        public void Create(Errand errand)
        {
            int errandId = _databaseService.AddErrandToDatabase(errand);
            errand.Id = errandId;
            _storeService.AddErrandToStore(errand);
            CreateErrandStatus(errandId);
        }
        private void CreateErrandStatus(int errandId)
        {
            string defaultReason = "SYSTEM";
            ErrandStatus status = new()
            {
                ErrandId = errandId,
                StatusName = ErrandStatusList.NoPartsList,
                SetDate = DateTime.Now,
                Reason = defaultReason
            };

            int statusId = _statusDatabaseService.AddErrandStatusToDatabase(status);
            status.Id = statusId;
            _statusStoreService.AddErrandStatusToStore(status);
        }
    }
}