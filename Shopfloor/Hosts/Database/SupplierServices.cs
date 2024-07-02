using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Interfaces;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Models.SupplierModel.Store.Combine;

namespace Shopfloor.Hosts.Database
{
    internal class SupplierServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<IProvider<Supplier>, SupplierProvider>();
            services.AddSingleton<IDataStore<Supplier>, SuppliersStore>();
            services.AddSingleton<ICombinerManager<Supplier>, SupplierCombiner>();
        }
    }
}