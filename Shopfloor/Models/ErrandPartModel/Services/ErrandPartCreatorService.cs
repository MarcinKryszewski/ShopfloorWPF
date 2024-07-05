using System;
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
using Shopfloor.Models.ErrandPartStatusModel;

namespace Shopfloor.Models.ErrandPartModel.Services
{
    internal class ErrandPartCreatorService : IModelCreatorService<ErrandPart>
    {
        private readonly IDataModelDatabaseService<ErrandPart> _databaseService;
        private readonly IDataModelStoreService<ErrandPart> _storeService;
        private readonly IModelCreatorService<ErrandPartStatus> _statusCreator;
        private readonly ICombinerManager<ErrandPart> _combiner;
        public ErrandPartCreatorService(
            IDataModelDatabaseService<ErrandPart> databaseService,
            IDataModelStoreService<ErrandPart> storeService,
            IModelCreatorService<ErrandPartStatus> statusCreator,
            ICombinerManager<ErrandPart> combiner)
        {
            _databaseService = databaseService;
            _storeService = storeService;
            _statusCreator = statusCreator;
            _combiner = combiner;
        }
        public void Create(ErrandPart item)
        {
            int id = _databaseService.Add(item);
            item.Id = id;
            _storeService.Add(item);
            CreateErrandPartStatus(item);
            _combiner.CombineOne(item);
        }
        private void CreateErrandPartStatus(ErrandPart item)
        {
            ErrandPartStatus status = new(0)
            {
                ErrandPartId = (int)item.Id!,
                CreatedDate = DateTime.Now,
            };
            _statusCreator.Create(status);
            item.StatusList.Add(status);
        }
    }
}