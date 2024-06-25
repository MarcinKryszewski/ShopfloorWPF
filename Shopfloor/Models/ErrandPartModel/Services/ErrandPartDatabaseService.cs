using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandPartModel.Services
{
    internal class ErrandPartDatabaseService : IDataModelDatabaseService<ErrandPart>
    {
        private readonly IProvider<ErrandPart> _provider;
        public ErrandPartDatabaseService(IProvider<ErrandPart> provider)
        {
            _provider = provider;
        }
        public int AddToDatabase(ErrandPart item) => _provider.Create(item).Result;
    }
}