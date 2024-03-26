using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartCombiner : ICombinerManager<ErrandPart>
    {
        private readonly ErrandPartToErrandPartStatus _errandPartStatusToErrandPart;
        private readonly List<ErrandPart> _data;
        private readonly ErrandPartToErrand _errandToErrandPart;
        private readonly ErrandPartToPart _partToErrandPart;
        private readonly ErrandPartToUser _userToErrandPart;
        public ErrandPartCombiner(ErrandPartStore store, ErrandPartToUser userToErrandPart, ErrandPartToPart partToErrandPart, ErrandPartToErrand errandToErrandPart, ErrandPartToErrandPartStatus errandPartStatusToErrandPart)
        {
            _userToErrandPart = userToErrandPart;
            _partToErrandPart = partToErrandPart;
            _errandToErrandPart = errandToErrandPart;
            _errandPartStatusToErrandPart = errandPartStatusToErrandPart;
            _data = store.GetData();
        }
        public async Task Combine()
        {
            List<Task> tasks = [];

            tasks.Add(_userToErrandPart.Combine(_data));
            tasks.Add(_partToErrandPart.Combine(_data));
            tasks.Add(_errandToErrandPart.Combine(_data));
            tasks.Add(_errandPartStatusToErrandPart.Combine(_data));

            await Task.WhenAll(tasks);
        }
    }
}