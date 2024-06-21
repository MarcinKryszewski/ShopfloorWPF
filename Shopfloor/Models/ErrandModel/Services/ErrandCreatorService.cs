using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandStatusModel.Services;
using System;

namespace Shopfloor.Models.ErrandModel.Services
{
    internal interface IErrandCreatorService
    {
        public void Create(Errand item);
    }
    internal class ErrandCreatorService : IErrandCreatorService
    {
        private readonly IErrandDatabaseService _databaseService;
        private readonly IErrandStoreService _storeService;
        private readonly IErrandStatusDatabaseService _statusDatabaseService;
        private readonly IErrandStatusStoreService _statusStoreService;
        public ErrandCreatorService(
            IErrandDatabaseService errandDatabaseService,
            IErrandStoreService errandStoreService,
            IErrandStatusDatabaseService errandStatusDatabaseService,
            IErrandStatusStoreService errandStatusStoreService)
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