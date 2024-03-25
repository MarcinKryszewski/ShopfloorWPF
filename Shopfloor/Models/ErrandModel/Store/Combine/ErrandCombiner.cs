using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandCombiner
    {
        private readonly ErrandToErrandPart _errandPart;
        private readonly ErrandToErrandStatus _errandStatus;
        private readonly ErrandToErrandType _errandType;
        private readonly ErrandToUser _user;
        private readonly ErrandToMachine _machine;
        public ErrandCombiner(ErrandToErrandPart errandPart, ErrandToErrandStatus errandStatus, ErrandToErrandType errandType, ErrandToUser user, ErrandToMachine machine)
        {
            _errandPart = errandPart;
            _errandStatus = errandStatus;
            _errandType = errandType;
            _user = user;
            _machine = machine;
        }
        public async Task Combine()
        {
            List<Task> tasks = [];

            tasks.Add(_errandPart.Combine());
            tasks.Add(_errandStatus.Combine());
            tasks.Add(_errandType.Combine());
            tasks.Add(_user.Combine());
            tasks.Add(_machine.Combine());

            await Task.WhenAll(tasks);
        }
    }
}