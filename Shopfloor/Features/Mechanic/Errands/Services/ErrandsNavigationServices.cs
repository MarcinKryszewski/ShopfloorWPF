using Microsoft.Extensions.DependencyInjection;
using System;

namespace Shopfloor.Features.Mechanic.Errands.Services
{
    internal sealed class ErrandsNavigationServices
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            //GetListNavigation(services, databaseServices, userServices);
            //GetAddNavigation(services, databaseServices, userServices);
            //GetEditNavigation(services, databaseServices, userServices);
            //GetPartsListNavigation(services, databaseServices);
        }
        /*private static void GetListNavigation(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            services.AddTransient((s) => CreateListViewModel(s, databaseServices, userServices));
            services.AddSingleton<CreateViewModel<ErrandsListViewModel>>((s) => () => s.GetRequiredService<ErrandsListViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<ErrandsListViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<ErrandsListViewModel>>()
                );
            });
        }
        private static void GetAddNavigation(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            services.AddTransient((s) => CreateAddViewModel(s, databaseServices, userServices));
            services.AddSingleton<CreateViewModel<ErrandsNewViewModel>>((s) => () => s.GetRequiredService<ErrandsNewViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<ErrandsNewViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<ErrandsNewViewModel>>()
                );
            });
        }
        private static void GetEditNavigation(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            services.AddTransient((s) => CreateEditViewModel(s, databaseServices, userServices));
            services.AddSingleton<CreateViewModel<ErrandsEditViewModel>>((s) => () => s.GetRequiredService<ErrandsEditViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<ErrandsEditViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<ErrandsEditViewModel>>()
                );
            });
        }
        private static void GetPartsListNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreatePartsListNavigation(s, databaseServices));
            services.AddSingleton<CreateViewModel<ErrandPartsListViewModel>>((s) => () => s.GetRequiredService<ErrandPartsListViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<ErrandPartsListViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<ErrandPartsListViewModel>>()
                );
            });
        }
        private static ErrandsListViewModel CreateListViewModel(IServiceProvider mainServices, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            return new ErrandsListViewModel(mainServices, databaseServices, userServices);
        }
        private static ErrandsEditViewModel CreateEditViewModel(IServiceProvider mainServices, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            return new ErrandsEditViewModel(mainServices, databaseServices, userServices);
        }
        private static ErrandsNewViewModel CreateAddViewModel(IServiceProvider mainServices, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            return new ErrandsNewViewModel(mainServices, databaseServices, userServices);
        }
        private static ErrandPartsListViewModel CreatePartsListNavigation(IServiceProvider mainServices, IServiceProvider databaseServices)
        {
            return new ErrandPartsListViewModel(mainServices, databaseServices);
        }*/
    }
}