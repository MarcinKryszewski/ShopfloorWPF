﻿using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Login;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Services.NavigationServices
{
    internal sealed class LoginNavigationServices
    {
        public static void Get(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userProvider)
        {
            GetLoginNavigation(services, databaseServices, userProvider);
        }

        public static void GetLoginNavigation(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userProvider)
        {
            services.AddTransient((s) => CreateLoginViewModel(s, databaseServices, userProvider));
            services.AddSingleton<CreateViewModel<LoginViewModel>>((s) => () => s.GetRequiredService<LoginViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<LoginViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<LoginViewModel>>()
                );
            });
        }

        private static LoginViewModel CreateLoginViewModel(IServiceProvider mainServices, IServiceProvider databaseServices, IServiceProvider userProvider)
        {
            return new LoginViewModel(mainServices, databaseServices, userProvider);
        }
    }
}