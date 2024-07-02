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
        public int AddToDatabase(Errand item) => _provider.Create(item).Result;

        public void EditInDatabase(Errand item)
        {
            Task.Run(() => _provider.Update(item));
        }
    }
}