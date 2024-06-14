using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Admin.Machines;
using Shopfloor.Features.Admin.Parts;
using Shopfloor.Features.Admin.Parts.Stores;
using Shopfloor.Features.Admin.PartTypes;
using Shopfloor.Features.Admin.Suppliers;
using Shopfloor.Features.Admin.Users;
using Shopfloor.Features.Admin.Users.Stores;

namespace Shopfloor.Hosts.Features.Admin
{
    internal sealed class AdminHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<UsersListViewModel>();
            services.AddTransient<UsersAddViewModel>();
            services.AddTransient<UsersEditViewModel>();
            services.AddSingleton<SelectedUserStore>();

            services.AddSingleton<MachinesListViewModel>();

            services.AddSingleton<PartsListViewModel>();
            services.AddSingleton<PartsAddViewModel>();
            services.AddSingleton<PartsEditViewModel>();

            services.AddSingleton<SuppliersListViewModel>();

            services.AddSingleton<PartTypesListViewModel>();

            services.AddSingleton<SelectedPartStore>();
        }
    }
}