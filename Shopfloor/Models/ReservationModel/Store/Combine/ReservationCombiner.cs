using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ReservationModel.Store.Combine
{
    internal sealed class ReservationCombiner : ICombinerManager<Reservation>
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
        public Task CombineOne(Reservation item)
        {
            return Task.CompletedTask;
        }
    }
}