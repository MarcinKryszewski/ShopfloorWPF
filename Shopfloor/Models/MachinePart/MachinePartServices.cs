using Microsoft.Extensions.DependencyInjection;

namespace Shopfloor.Models.MachinePartModel
{
    public class MachinePartServices
    {
        public static void GetServices(IServiceCollection services)
        {
            services.AddSingleton<MachinePartProvider>();
            services.AddSingleton<MachinePartStore>();
        }
    }
}