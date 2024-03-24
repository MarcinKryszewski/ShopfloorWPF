using Microsoft.Extensions.DependencyInjection;
using System;

namespace Shopfloor.Features.Admin.Parts.Services
{
    internal sealed class PartsNavigationServices
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices)
        {
            //GetListNavigation(services, databaseServices);
            //GetAddNavigation(services, databaseServices);
            //GetEditNavigation(services, databaseServices);
        }

        /*private static void GetListNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreatePartsListViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<PartsListViewModel>>((s) => () => s.GetRequiredService<PartsListViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<PartsListViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<PartsListViewModel>>()
                );
            });
        }

        private static void GetAddNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreatePartsAddViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<PartsAddViewModel>>((s) => () => s.GetRequiredService<PartsAddViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<PartsAddViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<PartsAddViewModel>>()
                );
            });
        }

        private static void GetEditNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreatePartsEditViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<PartsEditViewModel>>((s) => () => s.GetRequiredService<PartsEditViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<PartsEditViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<PartsEditViewModel>>()
                );
            });
        }

        private static PartsListViewModel CreatePartsListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            return new PartsListViewModel(mainServices, databaseServices);
        }

        private static PartsAddViewModel CreatePartsAddViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            return new PartsAddViewModel(mainServices, databaseServices);
        }

        private static PartsEditViewModel CreatePartsEditViewModel(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            return new PartsEditViewModel(mainServices, databaseServices);
        }*/
    }
}