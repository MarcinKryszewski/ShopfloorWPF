using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.OfferModel;
using Shopfloor.Models.OfferModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal class OfferServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IProvider<Offer>, OfferProvider>();
            services.AddSingleton<IDataStore<Offer>, OfferStore>();
            services.AddSingleton<ICombinerManager<Offer>, OfferCombiner>();
        }
    }

}