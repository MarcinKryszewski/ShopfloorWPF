using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.MachinePartModel;
using Shopfloor.Models.MachinePartModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal static class MachinePartServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IProvider<MachinePart>, MachinePartProvider>();
            services.AddSingleton<IDataStore<MachinePart>, MachinePartStore>();
            services.AddSingleton<ICombinerManager<MachinePart>, MachinePartCombiner>();
        }
    }
}