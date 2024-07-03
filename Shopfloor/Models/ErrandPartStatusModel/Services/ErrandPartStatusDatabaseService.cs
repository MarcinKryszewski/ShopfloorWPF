using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandPartStatusModel.Services
{
    internal class ErrandPartStatusDatabaseService : IDataModelDatabaseService<ErrandPartStatus>
    {
        private readonly IProvider<ErrandPartStatus> _provider;
        public ErrandPartStatusDatabaseService(IProvider<ErrandPartStatus> provider)
        {
            _provider = provider;
        }
        public int Add(ErrandPartStatus item) => _provider.Create(item).Result;

        public void Delete(ErrandPartStatus item)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(ErrandPartStatus item)
        {
            throw new System.NotImplementedException();
        }
    }
}