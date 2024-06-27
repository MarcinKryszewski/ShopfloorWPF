using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ErrandStatusModel.Services
{
    internal class ErrandStatusEditorService : IModelEditorService<ErrandStatus>
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
            _databaseService.EditInDatabase(item);
        }
    }
    internal interface IModelEditorService<T> where T : DataModel
    {
        public void Edit(T item);
    }
}