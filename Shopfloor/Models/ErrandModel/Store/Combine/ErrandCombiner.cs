using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandModel.Store.Combine
{
    internal sealed class ErrandCombine : ICombiner
    {
        private readonly ErrandPartToErrand _errandPart;
        private readonly ErrandStatusToErrand _errandStatus;
        private readonly ErrandTypeToErrand _errandType;
        private readonly UserToErrand _user;
        private readonly MachineToErrand _machine;
        public ErrandCombine(ErrandPartToErrand errandPart, ErrandStatusToErrand errandStatus, ErrandTypeToErrand errandType, UserToErrand user, MachineToErrand machine)
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