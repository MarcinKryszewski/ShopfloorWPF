using Shopfloor.Interfaces;
using Shopfloor.Shared.BaseClasses;

namespace Shopfloor.Models.ReservationModel
{
    internal sealed class ReservationStore : StoreBase<Reservation>
    {
        public ReservationStore(IProvider<Reservation> provider) : base(provider)
        {
        }
    }
}