using Shopfloor.Interfaces;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartStatusModel.Store.Combine
{
    internal sealed class ErrandPartStatusCombiner : ICombinerManager<ErrandPartStatus>
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}