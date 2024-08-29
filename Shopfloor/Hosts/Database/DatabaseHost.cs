using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Database;
using Shopfloor.Services;

namespace Shopfloor.Hosts.Database
{
    internal static class DatabaseHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<DatabaseConnectionFactory>();
        }
    }
}