using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.ReservationModel.Store.Combine;

namespace Shopfloor.Models.ReservationModel
{
    internal sealed class ReservationStore : StoreBase<Reservation>
    {
        public ReservationStore(ReservationProvider provider, ReservationCombiner combiner) : base(provider, combiner)
        {

        }
    }
}