using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandStatusModel.Services
{
    internal class ErrandStatusEditorService
    {
        private readonly IDataModelDatabaseService<ErrandStatus> _databaseService;
        private readonly IDataModelStoreService<ErrandStatus> _storeService;

        public ErrandStatusEditorService(IDataModelStoreService<ErrandStatus> storeService, IDataModelDatabaseService<ErrandStatus> databaseService)
        {
            _storeService = storeService;
            _databaseService = databaseService;
        }
        public void Edit(ErrandStatus item)
        {
            _storeService.EditInStore(item);
        }
    }
}