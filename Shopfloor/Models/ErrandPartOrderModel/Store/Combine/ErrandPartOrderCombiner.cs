using Shopfloor.Interfaces;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartOrderModel.Store.Combine
{
    internal sealed class ErrandPartOrderCombiner : ICombinerManager<ErrandPartOrder>
    {
        public bool IsCombined { get; private set; }
        public Task Combine(bool shouldForce = false)
        {
            if (IsCombined && !shouldForce) return Task.CompletedTask;
            IsCombined = true;
            return Task.CompletedTask;
        }
    }
}