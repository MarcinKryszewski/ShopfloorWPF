using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal static class PartServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IProvider<Part>, PartProvider>();
            services.AddSingleton<IDataStore<Part>, PartStore>();
            services.AddSingleton<ICombinerManager<Part>, PartCombiner>();

            services.AddSingleton<PartToPartType>();
        }
    }
}