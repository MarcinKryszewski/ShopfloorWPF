using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;

namespace Shopfloor.Hosts.Core
{
    internal static class NavigationHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<INavigationStore, NavigationStore>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<NavigationService>();

            services.AddSingleton<ViewModelBaseDependecies>();

            services.AddSingleton<Func<Type, ViewModelBase>>(implementationFactory: serviceProvider => viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

            services.TryAdd(ServiceDescriptor.Singleton(typeof(INavigationCommand<>), typeof(NavigationCommand<>)));
        }
    }
}