using Shopfloor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopfloor.Models.ReservationModel.Store.Combine
{
    internal sealed class ReservationCombiner : ICombinerManager<Reservation>
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}