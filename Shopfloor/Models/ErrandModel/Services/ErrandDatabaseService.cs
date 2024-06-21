namespace Shopfloor.Models.ErrandModel.Services
{
    internal class ErrandDatabaseService
    {
        private readonly ErrandProvider _errandProvider;
        public ErrandDatabaseService(ErrandProvider errandProvider)
        {
            _errandProvider = errandProvider;
        }
        public int AddErrandToDatabase(Errand errand) => _errandProvider.Create(errand).Result;
    }
}