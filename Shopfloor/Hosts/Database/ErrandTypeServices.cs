using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.ErrandTypeModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal static class ErrandTypeServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IProvider<ErrandType>, ErrandTypeProvider>();
            services.AddSingleton<IDataStore<ErrandType>, ErrandTypeStore>();
            services.AddSingleton<ICombinerManager<ErrandType>, ErrandTypeCombiner>();
        }
    }
}