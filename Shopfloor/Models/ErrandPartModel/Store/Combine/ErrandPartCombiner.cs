using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartCombiner : ICombinerManager<ErrandPart>
    {
        private readonly ErrandPartToErrandPartStatus _errandPartStatusToErrandPart;
        private readonly ErrandPartToErrand _errandToErrandPart;
        private readonly ErrandPartToPart _partToErrandPart;
        private readonly ErrandPartToUser _userToErrandPart;
        public ErrandPartCombiner(
            ErrandPartToUser userToErrandPart,
            ErrandPartToPart partToErrandPart,
            ErrandPartToErrand errandToErrandPart,
            ErrandPartToErrandPartStatus errandPartStatusToErrandPart)
        {
            _userToErrandPart = userToErrandPart;
            _partToErrandPart = partToErrandPart;
            _errandToErrandPart = errandToErrandPart;
            _errandPartStatusToErrandPart = errandPartStatusToErrandPart;
        }
        public bool IsCombined { get; private set; }
        public async Task CombineAll(bool shouldForce = false)
        {
            if (IsCombined && !shouldForce)
            {
                return;
            }

            List<Task> tasks = [];

            tasks.Add(_userToErrandPart.CombineAll());
            tasks.Add(_partToErrandPart.CombineAll());
            tasks.Add(_errandToErrandPart.CombineAll());
            tasks.Add(_errandPartStatusToErrandPart.CombineAll());

            await Task.WhenAll(tasks);
            IsCombined = true;
        }
        public async Task CombineOne(ErrandPart item)
        {
            List<Task> tasks = [];

            tasks.Add(_userToErrandPart.CombineOne(item));
            tasks.Add(_partToErrandPart.CombineOne(item));
            tasks.Add(_errandToErrandPart.CombineOne(item));
            tasks.Add(_errandPartStatusToErrandPart.CombineOne(item));

            await Task.WhenAll(tasks);
        }
    }
}