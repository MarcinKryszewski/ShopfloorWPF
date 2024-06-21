namespace Shopfloor.Models.ErrandStatusModel.Services
{
    internal class ErrandStatusDatabaseService
    {
        private readonly ErrandStatusProvider _errandStatusProvider;
        public ErrandStatusDatabaseService(ErrandStatusProvider errandStatusProvider)
        {
            _errandStatusProvider = errandStatusProvider;
        }
        public int AddErrandStatusToDatabase(ErrandStatus errandStatus) => _errandStatusProvider.Create(errandStatus).Result;
    }
}