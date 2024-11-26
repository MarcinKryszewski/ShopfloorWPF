using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Services.AuthServices;

namespace Shopfloor.Hosts.Features.Services
{
    internal static class AuthServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<UserProvider>();
            services.AddSingleton<IUserContext>(new UserContext());
            services.AddSingleton<AuthService>();
        }
    }
}