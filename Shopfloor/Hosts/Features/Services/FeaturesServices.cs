using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.ActionCreate;
using Shopfloor.Features.ActionDetails;
using Shopfloor.Features.ActionEdit;
using Shopfloor.Features.ActionsList;
using Shopfloor.Features.MachineResponsibilities;
using Shopfloor.Features.MachineResponsibilityEdit;
using Shopfloor.Features.TrainingsList;

namespace Shopfloor.Hosts.Features.Services
{
    internal static class FeaturesServices
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<ActionsListViewModel>();
            services.AddTransient<ActionsFilter>();

            services.AddTransient<ActionDetailsViewModel>();
            services.AddTransient<ActionEditViewModel>();
            services.AddTransient<ActionCreateViewModel>();

            services.AddTransient<TrainingsListViewModel>();

            services.AddTransient<MachineResponsibilitiesViewModel>();
            services.AddTransient<MachineResponsibilityEditViewModel>();
        }
    }
}