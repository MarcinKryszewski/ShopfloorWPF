using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandModel.Services
{
    internal interface IErrandDatabaseService
    {
        public int AddErrandToDatabase(Errand item);
    }
    internal class ErrandDatabaseService : IErrandDatabaseService
    {
        private readonly IProvider<Errand> _errandProvider;
        public ErrandDatabaseService(IProvider<Errand> errandProvider)
        {
            _errandProvider = errandProvider;
        }
        public int AddErrandToDatabase(Errand errand) => _errandProvider.Create(errand).Result;
    }
}