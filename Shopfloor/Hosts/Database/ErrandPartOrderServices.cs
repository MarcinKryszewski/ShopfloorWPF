using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartOrderModel;
using Shopfloor.Models.ErrandPartOrderModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal class ErrandPartOrderServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IProvider<ErrandPartOrder>, ErrandPartOrderProvider>();
            services.AddSingleton<IDataStore<ErrandPartOrder>, ErrandPartOrderStore>();
            services.AddSingleton<ICombinerManager<ErrandPartOrder>, ErrandPartOrderCombiner>();
        }
    }

}