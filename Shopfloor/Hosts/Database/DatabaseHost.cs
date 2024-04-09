using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Database;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel.Store.Combine;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartModel.Store.Combine;
using Shopfloor.Models.ErrandPartOfferModel;
using Shopfloor.Models.ErrandPartOfferModel.Store;
using Shopfloor.Models.ErrandPartOrderModel;
using Shopfloor.Models.ErrandPartOrderModel.Store.Combine;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandPartStatusModel.Store.Combine;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandStatusModel.Store.Combine;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.ErrandTypeModel.Store.Combine;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.MachineModel.Store;
using Shopfloor.Models.MachineModel.Store.Combine;
using Shopfloor.Models.MachinePartModel;
using Shopfloor.Models.MachinePartModel.Store.Combine;
using Shopfloor.Models.MessageModel;
using Shopfloor.Models.MessageModel.Store.Combine;
using Shopfloor.Models.OfferModel;
using Shopfloor.Models.OfferModel.Store.Combine;
using Shopfloor.Models.OrderModel;
using Shopfloor.Models.OrderModel.Store.Combine;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartModel.Store.Combine;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.PartTypeModel.Store.Combine;
using Shopfloor.Models.ReservationModel;
using Shopfloor.Models.ReservationModel.Store.Combine;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Models.SupplierModel.Store.Combine;
using Shopfloor.Models.UserModel;
using Shopfloor.Models.UserModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal sealed class DatabaseHost
    {
        public DatabaseHost()
        {
        }
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<DatabaseConnectionFactory>();
            ModelServices(services);
        }
        public static void ModelServices(IServiceCollection services)
        {
            ErrandServices(services);
            ErrandPartServices(services);
            ErrandPartOfferServices(services);
            ErrandPartOrderServices(services);
            ErrandPartStatusServices(services);
            ErrandStatusServices(services);
            ErrandTypeServices(services);
            MachineServices(services);
            MachinePartServices(services);
            MessageServices(services);
            OfferServices(services);
            OrderServices(services);
            PartServices(services);
            PartTypeServices(services);
            ReservationServices(services);
            RolePServices(services);
            RoleUserServices(services);
            SupplierServices(services);
            UserServices(services);
        }
        private static void ErrandServices(IServiceCollection services)
        {
            services.AddSingleton<ErrandStore, ErrandStore>();
            services.AddSingleton<ErrandStore>();
            services.AddSingleton<ErrandProvider>();
            services.AddSingleton<ErrandCombiner>();

            services.AddSingleton<ErrandToErrandPart>();
            services.AddSingleton<ErrandToErrandStatus>();
            services.AddSingleton<ErrandToUser>();
            services.AddSingleton<ErrandToMachine>();
            services.AddSingleton<ErrandToErrandType>();
        }
        private static void ErrandPartServices(IServiceCollection services)
        {
            services.AddSingleton<ErrandPartStore>();
            services.AddSingleton<ErrandPartProvider>();
            services.AddSingleton<ErrandPartCombiner>();

            services.AddSingleton<ErrandPartToErrandPartStatus>();
            services.AddSingleton<ErrandPartToErrand>();
            services.AddSingleton<ErrandPartToPart>();
            services.AddSingleton<ErrandPartToUser>();
        }
        private static void ErrandPartOfferServices(IServiceCollection services)
        {
            services.AddSingleton<ErrandPartOfferProvider>();
            services.AddSingleton<ErrandPartOfferStore>();
            services.AddSingleton<ErrandPartOfferCombiner>();
        }
        private static void ErrandPartOrderServices(IServiceCollection services)
        {
            services.AddSingleton<ErrandPartOrderProvider>();
            services.AddSingleton<ErrandPartOrderStore>();
            services.AddSingleton<ErrandPartOrderCombiner>();
        }
        private static void ErrandPartStatusServices(IServiceCollection services)
        {
            services.AddSingleton<ErrandPartStatusStore>();
            services.AddSingleton<ErrandPartStatusProvider>();
            services.AddSingleton<ErrandPartStatusCombiner>();
        }
        private static void ErrandStatusServices(IServiceCollection services)
        {
            services.AddSingleton<ErrandStatusProvider>();
            services.AddSingleton<ErrandStatusStore>();
            services.AddSingleton<ErrandStatusCombiner>();
        }
        private static void ErrandTypeServices(IServiceCollection services)
        {
            services.AddSingleton<ErrandTypeStore>();
            services.AddSingleton<ErrandTypeProvider>();
            services.AddSingleton<ErrandTypeCombiner>();
        }
        private static void MachineServices(IServiceCollection services)
        {
            services.AddSingleton<MachineProvider>();
            services.AddSingleton<MachineStore>();
            services.AddSingleton<MachineCombiner>();

            services.AddSingleton<MachineToMachine>();
        }
        private static void MachinePartServices(IServiceCollection services)
        {
            services.AddSingleton<MachinePartProvider>();
            services.AddSingleton<MachinePartStore>();
            services.AddSingleton<MachinePartCombiner>();
        }
        private static void MessageServices(IServiceCollection services)
        {
            services.AddSingleton<MessageProvider>();
            services.AddSingleton<MessageStore>();
            services.AddSingleton<MessageCombiner>();
        }
        private static void OfferServices(IServiceCollection services)
        {
            services.AddSingleton<OfferProvider>();
            services.AddSingleton<OfferStore>();
            services.AddSingleton<OfferCombiner>();
        }
        private static void OrderServices(IServiceCollection services)
        {
            services.AddSingleton<OrderProvider>();
            services.AddSingleton<OrderCombiner>();
            services.AddSingleton<OrderStore>();
        }
        private static void PartServices(IServiceCollection services)
        {
            services.AddSingleton<PartProvider>();
            services.AddSingleton<PartStore>();
            services.AddSingleton<PartCombiner>();

            services.AddSingleton<PartToPartType>();
        }
        private static void PartTypeServices(IServiceCollection services)
        {
            services.AddSingleton<PartTypeProvider>();
            services.AddSingleton<PartTypeStore>();
            services.AddSingleton<PartTypeCombiner>();
        }
        private static void ReservationServices(IServiceCollection services)
        {
            services.AddSingleton<ReservationProvider>();
            services.AddSingleton<ReservationStore>();
            services.AddSingleton<ReservationCombiner>();
        }
        private static void RolePServices(IServiceCollection services)
        {
            services.AddSingleton<RoleStore>();
            services.AddSingleton<IProvider<Role>, RoleProvider>();
        }
        private static void RoleUserServices(IServiceCollection services)
        {
            services.AddTransient<IProvider<RoleUser>, RoleUserProvider>();
            services.AddTransient<IRoleUserProvider, RoleUserProvider>();
            services.AddSingleton<RoleUserStore>();
        }
        private static void SupplierServices(IServiceCollection services)
        {
            services.AddSingleton<SuppliersStore>();
            services.AddSingleton<SupplierProvider>();
            services.AddSingleton<SupplierCombiner>();
        }
        private static void UserServices(IServiceCollection services)
        {
            services.AddSingleton<UserStore>();
            services.AddSingleton<UserProvider>();
            services.AddSingleton<UserCombiner>();
        }
    }
}