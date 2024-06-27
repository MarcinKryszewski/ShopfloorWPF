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
        public int AddToDatabase(ErrandPartStatus item) => _provider.Create(item).Result;

        public void EditInDatabase(ErrandPartStatus item)
        {
            throw new System.NotImplementedException();
        }
    }
}