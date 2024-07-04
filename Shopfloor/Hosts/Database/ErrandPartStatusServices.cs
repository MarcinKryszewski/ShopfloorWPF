using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandPartStatusModel.Services;
using Shopfloor.Models.ErrandPartStatusModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal static class ErrandPartStatusServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IDataStore<ErrandPartStatus>, ErrandPartStatusStore>();
            services.AddSingleton<IProvider<ErrandPartStatus>, ErrandPartStatusProvider>();
            services.AddSingleton<ICombinerManager<ErrandPartStatus>, ErrandPartStatusCombiner>();

            services.AddSingleton<IModelCreatorService<ErrandPartStatus>, ErrandPartStatusCreatorService>();
            services.AddSingleton<IDataModelDatabaseService<ErrandPartStatus>, ErrandPartStatusDatabaseService>();
            services.AddSingleton<IDataModelStoreService<ErrandPartStatus>, ErrandPartStatusStoreService>();
        }
    }
}