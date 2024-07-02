using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandModel.Services;
using Shopfloor.Models.ErrandModel.Store;
using Shopfloor.Models.ErrandModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal class ErrandServices
    {
        public static void Get(IServiceCollection services)
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
    }
}