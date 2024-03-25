using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.ReservationModel.Store.Combine
{
    public class ReservationCombiner : ICombiner
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}