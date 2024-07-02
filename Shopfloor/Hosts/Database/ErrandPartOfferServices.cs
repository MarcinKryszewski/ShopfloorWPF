using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandPartOfferModel;
using Shopfloor.Models.ErrandPartOfferModel.Store;

namespace Shopfloor.Hosts.Database
{
    public class ErrandPartOfferServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IDataStore<ErrandPartOffer>, ErrandPartOfferStore>();
            services.AddSingleton<IProvider<ErrandPartOffer>, ErrandPartOfferProvider>();
            services.AddSingleton<ICombinerManager<ErrandPartOffer>, ErrandPartOfferCombiner>();
        }
    }
}