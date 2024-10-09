using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Models.Manufacturers;
using Shopfloor.Models.Parts;
using Shopfloor.Models.PartTypes;
using Shopfloor.Models.WorkOrderParts;
using Shopfloor.Models.WorkOrders;

namespace Shopfloor.Hosts.Models
{
    internal static class ModelServices
    {
        public static void Get(IServiceCollection services)
        {
            WorkOrderServices(services);
            LinesServices(services);
            PartsServices(services);
            ManufacturersServices(services);
            PartTypesServices(services);
            MachinesServices(services);
            WorkOrderPartServices(services);
        }
        private static void WorkOrderPartServices(IServiceCollection services)
        {
            services.AddSingleton<IRepository<WorkOrderPartModel, WorkOrderPartCreationModel>, WorkOrderPartRepository>();
            services.AddSingleton<IStore<WorkOrderPartModel>, WorkOrderPartStore>();
            services.AddSingleton<IProvider<WorkOrderPartModel, WorkOrderPartCreationModel>, WorkOrderPartProvider>();
        }
        private static void LinesServices(IServiceCollection services)
        {
            services.AddSingleton<IRepository<LineModel, LineCreationModel>, LineRepository>();
            services.AddSingleton<IStore<LineModel>, LineStore>();
        }
        private static void WorkOrderServices(IServiceCollection services)
        {
            services.AddSingleton<IRepository<WorkOrderModel, WorkOrderCreationModel>, WorkOrderRepository>();
            services.AddSingleton<IStore<WorkOrderModel>, WorkOrderStore>();
        }
        private static void PartsServices(IServiceCollection services)
        {
            services.AddSingleton<IRepository<PartModel, PartCreationModel>, PartRepository>();
            services.AddSingleton<IStore<PartModel>, PartStore>();
        }
        private static void MachinesServices(IServiceCollection services)
        {
            services.AddSingleton<IRepository<MachineModel, MachineCreationModel>, MachineRepository>();
            services.AddSingleton<IStore<MachineModel>, MachineStore>();
        }
        private static void PartTypesServices(IServiceCollection services)
        {
            services.AddSingleton<IRepository<PartTypeModel, PartTypeCreationModel>, PartTypeRepository>();
            services.AddSingleton<IStore<PartTypeModel>, PartTypeStore>();
        }
        private static void ManufacturersServices(IServiceCollection services)
        {
            services.AddSingleton<IRepository<ManufacturerModel, ManufacturerCreationModel>, ManufacturerRepository>();
            services.AddSingleton<IStore<ManufacturerModel>, ManufacturerStore>();
        }
    }
}