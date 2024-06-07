using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Hosts.Core
{
    internal class NavigationHost
    {
        public static void Get(IServiceCollection services)
        {
            services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));
            services.AddSingleton<INavigationStore, NavigationStore>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<NavigationService>();
            services.TryAdd(ServiceDescriptor.Singleton(typeof(INavigationCommand<>), typeof(NavigationCommand<>)));
        }
    }
}