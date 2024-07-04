using System;
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
using Shopfloor.Models.ErrandPartStatusModel;

namespace Shopfloor.Models.ErrandPartModel.Services
{
    internal class ErrandPartEditorService : IModelEditorService<ErrandPart>
    {
        private readonly IDataModelDatabaseService<ErrandPart> _databaseService;
        private readonly IDataModelStoreService<ErrandPart> _storeService;
        private readonly IModelCreatorService<ErrandPartStatus> _statusCreator;
        public ErrandPartEditorService(
            IModelCreatorService<ErrandPartStatus> statusCreator,
            IDataModelStoreService<ErrandPart> storeService,
            IDataModelDatabaseService<ErrandPart> databaseService)
        {
            _statusCreator = statusCreator;
            _storeService = storeService;
            _databaseService = databaseService;
        }
        public void Edit(ErrandPart item)
        {
            _databaseService.Edit(item);
            ReplaceInStore(item);
            CreateNewStatus(item);
        }
        private void ReplaceInStore(ErrandPart item) => _storeService.Edit(item);
        private void CreateNewStatus(ErrandPart item)
        {
            string defaultReason = "ERRAND-PART EDITED";
            if (item.Id is null)
            {
                return;
            }

            ErrandPartStatus status = new (ErrandPartStatus.Status[2])
            {
                ErrandPartId = (int)item.Id,
                CreatedDate = DateTime.Now,
                Reason = defaultReason,
            };

            item.StatusList.Add(status);
            _statusCreator.Create(status);
        }
    }
}