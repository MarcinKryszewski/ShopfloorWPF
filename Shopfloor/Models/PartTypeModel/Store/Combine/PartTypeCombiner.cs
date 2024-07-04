using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.PartTypeModel.Store.Combine
{
    internal sealed class PartTypeCombiner : ICombinerManager<PartType>
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
        public Task CombineOne(PartType item)
        {
            return Task.CompletedTask;
        }
    }
}