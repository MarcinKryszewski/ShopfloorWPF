using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandModel.Services
{
    internal class ErrandDatabaseService : IDataModelDatabaseService<Errand>
    {
        private readonly IProvider<Errand> _provider;
        public ErrandDatabaseService(IProvider<Errand> provider)
        {
            _provider = provider;
        }
        public int Add(Errand item) => _provider.Create(item).Result;

        public void Delete(Errand item)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(Errand item)
        {
            Task.Run(() => _provider.Update(item));
        }
    }
}