using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandStatusModel.Store.Combine
{
    internal sealed class ErrandStatusCombiner : ICombinerManager<ErrandStatus>
    {
        public bool IsCombined { get; private set; }
        public Task CombineAll(bool shouldForce = false)
        {
            if (IsCombined && !shouldForce)
            {
                return Task.CompletedTask;
            }

            IsCombined = true;
            return Task.CompletedTask;
        }

        public Task CombineOne(ErrandStatus item)
        {
            return Task.CompletedTask;
        }
    }
}