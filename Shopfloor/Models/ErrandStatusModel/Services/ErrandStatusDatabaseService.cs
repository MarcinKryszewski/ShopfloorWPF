using Shopfloor.Interfaces;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandStatusModel.Services
{
    internal class ErrandStatusDatabaseService : IDataModelDatabaseService<ErrandStatus>
    {
        private readonly IProvider<ErrandStatus> _errandStatusProvider;
        public ErrandStatusDatabaseService(IProvider<ErrandStatus> errandStatusProvider)
        {
            _errandStatusProvider = errandStatusProvider;
        }
        public int AddToDatabase(ErrandStatus item) => _errandStatusProvider.Create(item).Result;
        public void EditInDatabase(ErrandStatus item) => Task.Run(() => _errandStatusProvider.Update(item));
    }
}