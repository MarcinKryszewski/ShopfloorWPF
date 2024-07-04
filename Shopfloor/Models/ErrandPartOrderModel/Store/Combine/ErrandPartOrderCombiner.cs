using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ErrandPartOrderModel.Store.Combine
{
    internal sealed class ErrandPartOrderCombiner : ICombinerManager<ErrandPartOrder>
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

        public Task CombineOne(ErrandPartOrder item)
        {
            return Task.CompletedTask;
        }
    }
}