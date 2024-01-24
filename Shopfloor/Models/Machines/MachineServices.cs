using Microsoft.Extensions.DependencyInjection;

namespace Shopfloor.Models.MachineModel
{
    public class MachineServices
    {
        public static void GetServices(IServiceCollection services)
        {
            services.AddSingleton<MachineProvider>();
            services.AddSingleton<MachineStore>();
        }
    }
}