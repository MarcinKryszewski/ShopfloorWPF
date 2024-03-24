using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Database;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel.Store.Combine;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartOfferModel;
using Shopfloor.Models.ErrandPartOrderModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.MachinePartModel;
using Shopfloor.Models.MessageModel;
using Shopfloor.Models.OfferModel;
using Shopfloor.Models.OrderModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.ReservationModel;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Models.UserModel;

namespace Shopfloor.Hosts.Database
{
    internal sealed class DatabaseHost
    {
        private readonly IConfiguration _configuration;

        public DatabaseHost(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<DatabaseConnectionFactory>();
            ModelServices(services);

            ProvidersServices(services);
            StoresServices(services);
        }
        public static void ModelServices(IServiceCollection services)
        {
            ErrandServices(services);
        }
        private static void StoresServices(IServiceCollection services)
        {
            services.AddSingleton<ErrandPartStore>();
            services.AddSingleton<ErrandPartOfferStore>();
            services.AddSingleton<ErrandPartOrderStore>();
            services.AddSingleton<ErrandPartStatusStore>();
            services.AddSingleton<ErrandStatusStore>();
            services.AddSingleton<ErrandTypeStore>();
            services.AddSingleton<MachineStore>();
            services.AddSingleton<MachinePartStore>();
            services.AddSingleton<MessageStore>();
            services.AddSingleton<OfferStore>();
            services.AddSingleton<OrderStore>();
            services.AddSingleton<PartStore>();
            services.AddSingleton<PartTypeStore>();
            services.AddSingleton<ReservationStore>();
            //services.AddSingleton<RoleStore>();
            //services.AddSingleton<RoleUserStore>();
            services.AddSingleton<Supplier>();
            services.AddSingleton<UserStore>();
        }

        private static void ProvidersServices(IServiceCollection services)
        {
            services.AddSingleton<ErrandPartProvider>();
            services.AddSingleton<ErrandPartOfferProvider>();
            services.AddSingleton<ErrandPartOrderProvider>();
            services.AddSingleton<ErrandPartStatusProvider>();
            services.AddSingleton<ErrandStatusProvider>();
            services.AddSingleton<ErrandTypeProvider>();
            services.AddSingleton<MachineProvider>();
            services.AddSingleton<MachinePartProvider>();
            services.AddSingleton<MessageProvider>();
            services.AddSingleton<OfferProvider>();
            services.AddSingleton<OrderProvider>();
            services.AddSingleton<PartProvider>();
            services.AddSingleton<PartTypeProvider>();
            services.AddSingleton<ReservationProvider>();
            services.AddSingleton<RoleProvider>();
            services.AddSingleton<RoleUserProvider>();
            services.AddSingleton<SupplierProvider>();
            services.AddSingleton<UserProvider>();
        }
        private static void ErrandServices(IServiceCollection services)
        {
            services.AddSingleton<ErrandStore>();
            services.AddSingleton<ErrandProvider>();
            services.AddSingleton<ErrandCombine>();

            services.AddSingleton<ErrandPartToErrand>();
            services.AddSingleton<ErrandStatusToErrand>();
            services.AddSingleton<UserToErrand>();
            services.AddSingleton<MachineToErrand>();
            services.AddSingleton<ErrandTypeToErrand>();
        }
    }
}