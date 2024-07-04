using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Interfaces.Models;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandStatusModel.Services;
using Shopfloor.Models.ErrandStatusModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal static class ErrandStatusServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IProvider<ErrandStatus>, ErrandStatusProvider>();
            services.AddSingleton<IDataStore<ErrandStatus>, ErrandStatusStore>();
            services.AddSingleton<ICombinerManager<ErrandStatus>, ErrandStatusCombiner>();

            services.AddSingleton<IModelCreatorService<ErrandStatus>, ErrandStatusCreatorService>();
            services.AddSingleton<IDataModelDatabaseService<ErrandStatus>, ErrandStatusDatabaseService>();
            services.AddSingleton<IDataModelStoreService<ErrandStatus>, ErrandStatusStoreService>();
            services.AddSingleton<IModelEditorService<ErrandStatus>, ErrandStatusEditorService>();
        }
    }
}