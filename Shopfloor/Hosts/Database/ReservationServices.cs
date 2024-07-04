using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.ReservationModel;
using Shopfloor.Models.ReservationModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal static class ReservationServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IProvider<Reservation>, ReservationProvider>();
            services.AddSingleton<IDataStore<Reservation>, ReservationStore>();
            services.AddSingleton<ICombinerManager<Reservation>, ReservationCombiner>();
        }
    }
}