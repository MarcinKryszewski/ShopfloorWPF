using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandCombiner : ICombinerManager<Errand>
    {
        private readonly ErrandToErrandPart _errandPart;
        private readonly ErrandToErrandStatus _errandStatus;
        private readonly ErrandToErrandType _errandType;
        private readonly ErrandToMachine _machine;
        private readonly ErrandToUser _user;
        public ErrandCombiner(ErrandToErrandPart errandPart, ErrandToErrandStatus errandStatus, ErrandToErrandType errandType, ErrandToUser user, ErrandToMachine machine)
        {
            _errandPart = errandPart;
            _errandStatus = errandStatus;
            _errandType = errandType;
            _user = user;
            _machine = machine;
        }
        public bool IsCombined { get; private set; }
        public async Task CombineAll(bool shouldForce = false)
        {
            if (IsCombined && !shouldForce)
            {
                return;
            }

            List<Task> tasks = [];

            tasks.Add(_errandPart.CombineAll());
            tasks.Add(_errandStatus.CombineAll());
            tasks.Add(_errandType.CombineAll());
            tasks.Add(_user.CombineAll());
            tasks.Add(_machine.CombineAll());

            await Task.WhenAll(tasks);
            IsCombined = true;
        }
        public async Task CombineOne(Errand item)
        {
            List<Task> tasks = [];

            tasks.Add(_errandPart.CombineOne(item));
            tasks.Add(_errandStatus.CombineOne(item));
            tasks.Add(_errandType.CombineOne(item));
            tasks.Add(_user.CombineOne(item));
            tasks.Add(_machine.CombineOne(item));

            await Task.WhenAll(tasks);
        }
    }
}