using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Database;
using Shopfloor.Services;

namespace Shopfloor.Hosts.Database
{
    internal static class DatabaseHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<DatabaseConnectionFactory>();
            ModelServices(services);
            StoresServices(services);
        }
        public static void ModelServices(IServiceCollection services)
        {
            ErrandServices.Get(services);
            ErrandPartServices.Get(services);
            ErrandPartOfferServices.Get(services);
            ErrandPartOrderServices.Get(services);
            ErrandPartStatusServices.Get(services);
            ErrandStatusServices.Get(services);
            ErrandTypeServices.Get(services);
            MachineServices.Get(services);
            MachinePartServices.Get(services);
            MessageServices.Get(services);
            OfferServices.Get(services);
            OrderServices.Get(services);
            PartServices.Get(services);
            PartTypeServices.Get(services);
            ReservationServices.Get(services);
            RoleServices.Get(services);
            RoleUserServices.Get(services);
            SupplierServices.Get(services);
            UserServices.Get(services);
        }
        private static void StoresServices(IServiceCollection services)
        {
            services.AddSingleton<StoreRepository>();
        }
    }
}