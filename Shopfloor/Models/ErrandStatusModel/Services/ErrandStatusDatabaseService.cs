using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandStatusModel.Services
{
    internal class ErrandStatusDatabaseService : IDataModelDatabaseService<ErrandStatus>
    {
        private readonly ErrandStatusProvider _errandStatusProvider;
        public ErrandStatusDatabaseService(ErrandStatusProvider errandStatusProvider)
        {
            _errandStatusProvider = errandStatusProvider;
        }
        public int AddToDatabase(ErrandStatus item) => _errandStatusProvider.Create(item).Result;
        public void EditInDatabase(ErrandStatus item) => Task.Run(() => _errandStatusProvider.Update(item));
    }
}