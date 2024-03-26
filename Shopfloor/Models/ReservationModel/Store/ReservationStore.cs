namespace Shopfloor.Models.ReservationModel
{
    internal sealed class ReservationStore : StoreBase<Reservation>
    {
        public ReservationStore(ReservationProvider provider) : base(provider)
        {
        }
    }
}