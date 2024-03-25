using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.MachinePartModel.Store.Combine
{
    public class MachinePartCombiner : ICombiner
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}