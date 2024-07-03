using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartModel.Services;
using Shopfloor.Models.ErrandPartModel.Store;
using Shopfloor.Models.ErrandPartModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal class ErrandPartServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IDataStore<ErrandPart>, ErrandPartStore>();
            services.AddSingleton<IProvider<ErrandPart>, ErrandPartProvider>();
            services.AddSingleton<ICombinerManager<ErrandPart>, ErrandPartCombiner>();

            services.AddSingleton<ErrandPartToErrandPartStatus>();
            services.AddSingleton<ErrandPartToErrand>();
            services.AddSingleton<ErrandPartToPart>();
            services.AddSingleton<ErrandPartToUser>();

            services.AddSingleton<IModelCreatorService<ErrandPart>, ErrandPartCreatorService>();
            services.AddSingleton<IModelEditorService<ErrandPart>, ErrandPartEditorService>();
            services.AddSingleton<IModelDeleterService<ErrandPart>, ErrandPartDeleterService>();


            services.AddSingleton<IDataModelDatabaseService<ErrandPart>, ErrandPartDatabaseService>();
            services.AddSingleton<IDataModelStoreService<ErrandPart>, ErrandPartStoreService>();
            services.AddSingleton<IModelCrudService<ErrandPart>, ErrandPartCrudService>();

        }
    }
}