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
        public int Add(ErrandStatus item) => _errandStatusProvider.Create(item).Result;

        public void Delete(ErrandStatus item)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(ErrandStatus item) => Task.Run(() => _errandStatusProvider.Update(item));
    }
}