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
        public int Add(ErrandPart item) => _provider.Create(item).Result;

        public void Delete(ErrandPart item)
        {
            if (item.Id is null) return;
            _provider.Delete((int)item.Id);
        }

        public void Edit(ErrandPart item)
        {
            if (item.Id is null) return;
            _provider.Update(item);
        }
    }
}