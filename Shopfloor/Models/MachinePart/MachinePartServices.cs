using Microsoft.Extensions.DependencyInjection;

namespace Shopfloor.Models.MachinePartModel
{
    internal sealed class MachinePartServices
    {
        public static void GetServices(IServiceCollection services)
        {
            services.AddSingleton<MachinePartProvider>();
            services.AddSingleton<MachinePartStore>();
        }
    }
}