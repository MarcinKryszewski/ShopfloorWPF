using Shopfloor.Interfaces;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartStatusModel.Store.Combine
{
    internal sealed class ErrandPartStatusCombiner : ICombinerManager<ErrandPartStatus>
    {
        public bool IsCombined { get; private set; }
        public Task CombineAll(bool shouldForce = false)
        {
            if (IsCombined && !shouldForce) return Task.CompletedTask;
            IsCombined = true;
            return Task.CompletedTask;
        }

        public Task CombineOne(ErrandPartStatus item)
        {
            return Task.CompletedTask;
        }
    }
}