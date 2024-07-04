using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Login;
using Shopfloor.Stores;

namespace Shopfloor.Hosts.Features.Login
{
    internal static class LoginHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<ICurrentUserStore, CurrentUserStore>();
            services.AddSingleton<LoginViewModel>();
        }
    }
}