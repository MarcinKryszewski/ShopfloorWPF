using System;
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
using Shopfloor.Models.ErrandStatusModel;

namespace Shopfloor.Models.ErrandModel.Services
{
    internal class ErrandEditorService : IModelEditorService<Errand>
    {
        private readonly IDataModelDatabaseService<Errand> _databaseService;
        private readonly IDataModelStoreService<Errand> _storeService;
        private readonly IModelCreatorService<ErrandStatus> _statusCreator;
        public ErrandEditorService(
            IDataModelStoreService<Errand> storeService,
            IDataModelDatabaseService<Errand> databaseService,
            IModelCreatorService<ErrandStatus> statusCreator)
        {
            _storeService = storeService;
            _databaseService = databaseService;
            _statusCreator = statusCreator;
        }
        public void Edit(Errand item)
        {
            _databaseService.EditInDatabase(item);
            ReplaceInStore(item);
            CreateErrandStatus(item);
        }
        private void ReplaceInStore(Errand item)
        {
            _storeService.EditInStore(item);
        }
        private void CreateErrandStatus(Errand errand)
        {
            string defaultReason = "ERRAND EDITED";
            if (errand.Id is null) return;

            ErrandStatus status = new()
            {
                ErrandId = (int)errand.Id,
                StatusName = ErrandStatusList.ErrandEdited,
                SetDate = DateTime.Now,
                Reason = defaultReason
            };
            errand.AddStatus(status);
            _statusCreator.Create(status);
        }
    }
}