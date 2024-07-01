using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Database;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandModel.Services;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel.Store.Combine;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Services;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartModel.Store.Combine;
using Shopfloor.Models.ErrandPartOfferModel;
using Shopfloor.Models.ErrandPartOfferModel.Store;
using Shopfloor.Models.ErrandPartOrderModel;
using Shopfloor.Models.ErrandPartOrderModel.Store.Combine;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandPartStatusModel.Services;
using Shopfloor.Models.ErrandPartStatusModel.Store.Combine;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandStatusModel.Services;
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
using Shopfloor.Services;

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
            StoresServices(services);
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
            RoleServices(services);
            RoleUserServices(services);
            SupplierServices(services);
            UserServices(services);
        }
        private static void ErrandServices(IServiceCollection services)
        {
            services.AddSingleton<IDataStore<Errand>, ErrandStore>();
            services.AddSingleton<IProvider<Errand>, ErrandProvider>();
            services.AddSingleton<ICombinerManager<Errand>, ErrandCombiner>();

            services.AddSingleton<ErrandToErrandPart>();
            services.AddSingleton<ErrandToErrandStatus>();
            services.AddSingleton<ErrandToUser>();
            services.AddSingleton<ErrandToMachine>();
            services.AddSingleton<ErrandToErrandType>();

            services.AddSingleton<IModelCreatorService<Errand>, ErrandCreatorService>();
            services.AddSingleton<IDataModelDatabaseService<Errand>, ErrandDatabaseService>();
            services.AddSingleton<IDataModelStoreService<Errand>, ErrandStoreService>();
        }
        private static void ErrandPartServices(IServiceCollection services)
        {
            services.AddSingleton<IDataStore<ErrandPart>, ErrandPartStore>();
            services.AddSingleton<IProvider<ErrandPart>, ErrandPartProvider>();
            services.AddSingleton<ICombinerManager<ErrandPart>, ErrandPartCombiner>();

            services.AddSingleton<ErrandPartToErrandPartStatus>();
            services.AddSingleton<ErrandPartToErrand>();
            services.AddSingleton<ErrandPartToPart>();
            services.AddSingleton<ErrandPartToUser>();

            services.AddSingleton<IModelCreatorService<ErrandPart>, ErrandPartCreatorService>();
            services.AddSingleton<IDataModelDatabaseService<ErrandPart>, ErrandPartDatabaseService>();
            services.AddSingleton<IDataModelStoreService<ErrandPart>, ErrandPartStoreService>();
        }
        private static void ErrandPartOfferServices(IServiceCollection services)
        {
            services.AddSingleton<IDataStore<ErrandPartOffer>, ErrandPartOfferStore>();
            services.AddSingleton<IProvider<ErrandPartOffer>, ErrandPartOfferProvider>();
            services.AddSingleton<ICombinerManager<ErrandPartOffer>, ErrandPartOfferCombiner>();
        }
        private static void ErrandPartOrderServices(IServiceCollection services)
        {
            services.AddSingleton<IProvider<ErrandPartOrder>, ErrandPartOrderProvider>();
            services.AddSingleton<IDataStore<ErrandPartOrder>, ErrandPartOrderStore>();
            services.AddSingleton<ICombinerManager<ErrandPartOrder>, ErrandPartOrderCombiner>();
        }
        private static void ErrandPartStatusServices(IServiceCollection services)
        {
            services.AddSingleton<IDataStore<ErrandPartStatus>, ErrandPartStatusStore>();
            services.AddSingleton<IProvider<ErrandPartStatus>, ErrandPartStatusProvider>();
            services.AddSingleton<ICombinerManager<ErrandPartStatus>, ErrandPartStatusCombiner>();

            services.AddSingleton<IModelCreatorService<ErrandPartStatus>, ErrandPartStatusCreatorService>();
            services.AddSingleton<IDataModelDatabaseService<ErrandPartStatus>, ErrandPartStatusDatabaseService>();
            services.AddSingleton<IDataModelStoreService<ErrandPartStatus>, ErrandPartStatusStoreService>();
        }
        private static void ErrandStatusServices(IServiceCollection services)
        {
            services.AddSingleton<IProvider<ErrandStatus>, ErrandStatusProvider>();
            services.AddSingleton<IDataStore<ErrandStatus>, ErrandStatusStore>();
            services.AddSingleton<ICombinerManager<ErrandStatus>, ErrandStatusCombiner>();

            services.AddSingleton<IModelCreatorService<ErrandStatus>, ErrandStatusCreatorService>();
            services.AddSingleton<IDataModelDatabaseService<ErrandStatus>, ErrandStatusDatabaseService>();
            services.AddSingleton<IDataModelStoreService<ErrandStatus>, ErrandStatusStoreService>();
            services.AddSingleton<IModelEditorService<ErrandStatus>, ErrandStatusEditorService>();
        }
        private static void ErrandTypeServices(IServiceCollection services)
        {
            services.AddSingleton<IProvider<ErrandType>, ErrandTypeProvider>();
            services.AddSingleton<IDataStore<ErrandType>, ErrandTypeStore>();
            services.AddSingleton<ICombinerManager<ErrandType>, ErrandTypeCombiner>();
        }
        private static void MachineServices(IServiceCollection services)
        {
            services.AddSingleton<IProvider<Machine>, MachineProvider>();
            services.AddSingleton<IDataStore<Machine>, MachineStore>();
            services.AddSingleton<ICombinerManager<Machine>, MachineCombiner>();

            services.AddSingleton<MachineToMachine>();
        }
        private static void MachinePartServices(IServiceCollection services)
        {
            services.AddSingleton<IProvider<MachinePart>, MachinePartProvider>();
            services.AddSingleton<IDataStore<MachinePart>, MachinePartStore>();
            services.AddSingleton<ICombinerManager<MachinePart>, MachinePartCombiner>();
        }
        private static void MessageServices(IServiceCollection services)
        {
            services.AddSingleton<IProvider<Message>, MessageProvider>();
            services.AddSingleton<IDataStore<Message>, MessageStore>();
            services.AddSingleton<ICombinerManager<Message>, MessageCombiner>();
        }
        private static void OfferServices(IServiceCollection services)
        {
            services.AddSingleton<IProvider<Offer>, OfferProvider>();
            services.AddSingleton<IDataStore<Offer>, OfferStore>();
            services.AddSingleton<ICombinerManager<Offer>, OfferCombiner>();
        }
        private static void OrderServices(IServiceCollection services)
        {
            services.AddSingleton<IProvider<Order>, OrderProvider>();
            services.AddSingleton<IDataStore<Order>, OrderStore>();
            services.AddSingleton<ICombinerManager<Order>, OrderCombiner>();
        }
        private static void PartServices(IServiceCollection services)
        {
            services.AddSingleton<IProvider<Part>, PartProvider>();
            services.AddSingleton<IDataStore<Part>, PartStore>();
            services.AddSingleton<ICombinerManager<Part>, PartCombiner>();

            services.AddSingleton<PartToPartType>();
        }
        private static void PartTypeServices(IServiceCollection services)
        {
            services.AddSingleton<IProvider<PartType>, PartTypeProvider>();
            services.AddSingleton<IDataStore<PartType>, PartTypeStore>();
            services.AddSingleton<ICombinerManager<PartType>, PartTypeCombiner>();
        }
        private static void ReservationServices(IServiceCollection services)
        {
            services.AddSingleton<IProvider<Reservation>, ReservationProvider>();
            services.AddSingleton<IDataStore<Reservation>, ReservationStore>();
            services.AddSingleton<ICombinerManager<Reservation>, ReservationCombiner>();
        }
        private static void RoleServices(IServiceCollection services)
        {
            services.AddSingleton<IProvider<Role>, RoleProvider>();
            services.AddSingleton<IDataStore<Role>, RoleStore>();
            // services.AddSingleton<ICombinerManager<Role>, RoleCombiner>();
        }
        private static void RoleUserServices(IServiceCollection services)
        {
            // services.AddSingleton<IProvider<RoleUser>, RoleUserProvider>();
            services.AddSingleton<IDataStore<RoleUser>, RoleUserStore>();
            // services.AddSingleton<ICombinerManager<RoleUser>, RoleUserCombiner>();

            services.AddTransient<IProvider<RoleUser>, RoleIUserProvider>();
            services.AddTransient<IRoleIUserProvider, RoleIUserProvider>();

        }
        private static void SupplierServices(IServiceCollection services)
        {
            services.AddSingleton<IProvider<Supplier>, SupplierProvider>();
            services.AddSingleton<IDataStore<Supplier>, SuppliersStore>();
            services.AddSingleton<ICombinerManager<Supplier>, SupplierCombiner>();
        }
        private static void UserServices(IServiceCollection services)
        {
            services.AddSingleton<IProvider<User>, UserProvider>();
            services.AddSingleton<IDataStore<User>, UserStore>();
            services.AddSingleton<ICombinerManager<User>, UserCombiner>();

            services.AddSingleton<IUserProvider, UserProvider>();
        }
        private static void StoresServices(IServiceCollection services)
        {
            services.AddSingleton<StoreRepository>();
        }
    }
}