using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Database;

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