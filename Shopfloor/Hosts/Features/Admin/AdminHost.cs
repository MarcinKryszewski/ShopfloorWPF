using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Machines;
using Shopfloor.Features.Admin.Parts;
using Shopfloor.Features.Admin.PartTypes;
using Shopfloor.Features.Admin.Suppliers;
using Shopfloor.Features.Admin.Users;

namespace Shopfloor.Hosts.Features.Admin
{
    internal sealed class AdminHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<UsersMainViewModel>();
            services.AddSingleton<MachinesMainViewModel>();
            services.AddSingleton<PartsMainViewModel>();
            services.AddSingleton<SuppliersMainViewModel>();
            services.AddSingleton<PartTypesMainViewModel>();
        }
    }
}