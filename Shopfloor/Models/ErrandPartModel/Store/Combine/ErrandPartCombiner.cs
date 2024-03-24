using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartModel.Store.Combine
{
    internal sealed class ErrandPartCombiner : ICombiner
    {
        private readonly ErrandPartStatusToErrandPart _errandPartStatusToErrandPart;
        private readonly ErrandToErrandPart _errandToErrandPart;
        private readonly PartToErrandPart _partToErrandPart;
        private readonly UserToErrandPart _userToErrandPart;
        public ErrandPartCombiner(UserToErrandPart userToErrandPart, PartToErrandPart partToErrandPart, ErrandToErrandPart errandToErrandPart, ErrandPartStatusToErrandPart errandPartStatusToErrandPart)
        {
            _userToErrandPart = userToErrandPart;
            _partToErrandPart = partToErrandPart;
            _errandToErrandPart = errandToErrandPart;
            _errandPartStatusToErrandPart = errandPartStatusToErrandPart;
        }
        public async Task Combine()
        {
            List<Task> tasks = [];

            tasks.Add(_userToErrandPart.Combine());
            tasks.Add(_partToErrandPart.Combine());
            tasks.Add(_errandToErrandPart.Combine());
            tasks.Add(_errandPartStatusToErrandPart.Combine());

            await Task.WhenAll(tasks);
        }
    }
}