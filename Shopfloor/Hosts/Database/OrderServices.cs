using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.OrderModel;
using Shopfloor.Models.OrderModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal class OrderServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IProvider<Order>, OrderProvider>();
            services.AddSingleton<IDataStore<Order>, OrderStore>();
            services.AddSingleton<ICombinerManager<Order>, OrderCombiner>();
        }
    }
}