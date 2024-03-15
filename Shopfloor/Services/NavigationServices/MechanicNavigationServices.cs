using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Mechanic.Errands;
using Shopfloor.Features.Mechanic.MechanicDashboard;
using Shopfloor.Features.Mechanic.PartsStock;
using Shopfloor.Features.Mechanic.Requests;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Services.NavigationServices
{
    internal sealed class MechanicNavigationServices
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            GetDashboardNavigation(services);
            GetTasksNavigation(services, databaseServices, userServices);
            GetRequestsNavigation(services, databaseServices, userServices);
            GetPartsStockNavigation(services, databaseServices);
        }
        private static void GetDashboardNavigation(IServiceCollection services)
        {
            services.AddTransient((s) => CreateDashboardViewModel(s));
            services.AddSingleton<CreateViewModel<MechanicDashboardViewModel>>((s) => () => s.GetRequiredService<MechanicDashboardViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<MechanicDashboardViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<MechanicDashboardViewModel>>()
                );
            });
        }
        private static void GetRequestsNavigation(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            services.AddTransient((s) => CreatRequestsViewModel(s, databaseServices, userServices));
            services.AddSingleton<CreateViewModel<RequestsMainViewModel>>((s) => () => s.GetRequiredService<RequestsMainViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<RequestsMainViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<RequestsMainViewModel>>()
                );
            });
        }
        private static void GetTasksNavigation(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            services.AddTransient((s) => CreatTasksViewModel(s, databaseServices, userServices));
            services.AddSingleton<CreateViewModel<ErrandsMainViewModel>>((s) => () => s.GetRequiredService<ErrandsMainViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<ErrandsMainViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<ErrandsMainViewModel>>()
                );
            });
        }
        private static void GetPartsStockNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreatPartStockViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<PartsStockMainViewModel>>((s) => () => s.GetRequiredService<PartsStockMainViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<PartsStockMainViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<PartsStockMainViewModel>>()
                );
            });
        }
        private static MechanicDashboardViewModel CreateDashboardViewModel(IServiceProvider services) => new();
        private static RequestsMainViewModel CreatRequestsViewModel(IServiceProvider services, IServiceProvider databaseServices, IServiceProvider userServices) => new RequestsMainViewModel(databaseServices, userServices);
        private static ErrandsMainViewModel CreatTasksViewModel(IServiceProvider services, IServiceProvider databaseServices, IServiceProvider userServices) => new(databaseServices, userServices);
        private static PartsStockMainViewModel CreatPartStockViewModel(IServiceProvider services, IServiceProvider databaseServices) => new(databaseServices);
    }
}