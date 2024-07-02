using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.MachineModel.Store;
using Shopfloor.Models.MachineModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal class MachineServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IProvider<Machine>, MachineProvider>();
            services.AddSingleton<IDataStore<Machine>, MachineStore>();
            services.AddSingleton<ICombinerManager<Machine>, MachineCombiner>();

            services.AddSingleton<MachineToMachine>();
        }
    }

}