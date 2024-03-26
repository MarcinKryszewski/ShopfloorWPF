using Shopfloor.Interfaces;
using System.Threading.Tasks;

namespace Shopfloor.Models.ErrandPartOrderModel.Store.Combine
{
    internal sealed class ErrandPartOrderCombiner : ICombinerManager<ErrandPartOrder>
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}