using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Hosts.Features.Admin;
using Shopfloor.Hosts.Features.Layout;
using Shopfloor.Hosts.Features.Login;
using Shopfloor.Hosts.Features.Manager;
using Shopfloor.Hosts.Features.Mechanic;
using Shopfloor.Hosts.Features.Plannist;
using Shopfloor.Services;

namespace Shopfloor.Hosts.Features
{
    internal static class FeaturesHost
    {
        public static void Get(IServiceCollection services)
        {
            LayoutHost.Get(services);
            NotifierServices.Get(services);
            services.AddTransient<IAuthService, AuthService>();
            LoginHost.Get(services);

            AdminHost.Get(services);
            ManagerHost.Get(services);
            MechanicHost.Get(services);
            PlannistHost.Get(services);
        }
    }
}