using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ReservationModel.Store.Combine
{
    internal sealed class ReservationCombiner : ICombiner<Reservation>
    {
        public Task Combine(List<Reservation> data)
        {
            return Task.CompletedTask;
        }
    }
}