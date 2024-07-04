using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.PartTypeModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal static class PartTypeServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IProvider<PartType>, PartTypeProvider>();
            services.AddSingleton<IDataStore<PartType>, PartTypeStore>();
            services.AddSingleton<ICombinerManager<PartType>, PartTypeCombiner>();
        }
    }
}