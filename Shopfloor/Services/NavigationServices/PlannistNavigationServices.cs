using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Features.Plannist.Deploys;
using Shopfloor.Features.Plannist.Offers;
using Shopfloor.Features.Plannist.Offers.AddOffer;
using Shopfloor.Features.Plannist.PartsOrders;
using Shopfloor.Features.Plannist.PlannistDashboard.PlannistPartsList;
using Shopfloor.Features.Plannist.PlannistDashboard.Stores;
using Shopfloor.Features.Plannist.Reports;
using Shopfloor.Features.Plannist.Reservations;
using Shopfloor.Shared.Services;
using Shopfloor.Shared.Stores;
using Shopfloor.Shared.ViewModels;
using System;

namespace Shopfloor.Services.NavigationServices
{
    internal sealed class PlannistNavigationServices
    {
        internal static void Get(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            AddStoresToServices(services);
            GetPlannistDashboardMainNavigation(services, databaseServices);
            GetDeploysNavigation(services, databaseServices);
            GetOrdersNavigation(services, databaseServices);
            GetReportsNavigation(services, databaseServices);
            GetReservationsNavigation(services, databaseServices);
            GetOffersNavigation(services, databaseServices);
            GetAddOfferNavigation(services, databaseServices, userServices);
        }
        public static void AddStoresToServices(IServiceCollection services)
        {
            //SelectedRequestStore requestStore = new();
            services.AddSingleton<SelectedRequestStore>();
        }
        private static void GetPlannistDashboardMainNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreatePlannistDashboardMainViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<PlannistPartsListViewModel>>((s) => () => s.GetRequiredService<PlannistPartsListViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<PlannistPartsListViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<PlannistPartsListViewModel>>()
                );
            });
        }
        private static void GetAddOfferNavigation(IServiceCollection services, IServiceProvider databaseServices, IServiceProvider userServices)
        {
            services.AddTransient((s) => CreateAddOfferViewModel(s, databaseServices, userServices));
            services.AddSingleton<CreateViewModel<AddOfferViewModel>>((s) => () => s.GetRequiredService<AddOfferViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<AddOfferViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<AddOfferViewModel>>()
                );
            });
        }
        private static void GetDeploysNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateDeploysViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<DeploysViewModel>>((s) => () => s.GetRequiredService<DeploysViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<DeploysViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<DeploysViewModel>>()
                );
            });
        }
        private static void GetOrdersNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateOrdersViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<PartsOrdersViewModel>>((s) => () => s.GetRequiredService<PartsOrdersViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<PartsOrdersViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<PartsOrdersViewModel>>()
                );
            });
        }
        private static void GetOffersNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateOffersViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<OffersViewModel>>((s) => () => s.GetRequiredService<OffersViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<OffersViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<OffersViewModel>>()
                );
            });
        }
        private static void GetReportsNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateReportsViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<ReportsViewModel>>((s) => () => s.GetRequiredService<ReportsViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<ReportsViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<ReportsViewModel>>()
                );
            });
        }
        private static void GetReservationsNavigation(IServiceCollection services, IServiceProvider databaseServices)
        {
            services.AddTransient((s) => CreateReservationsViewModel(s, databaseServices));
            services.AddSingleton<CreateViewModel<ReservationsViewModel>>((s) => () => s.GetRequiredService<ReservationsViewModel>());
            services.AddSingleton((s) =>
            {
                return new NavigationService<ReservationsViewModel>(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<CreateViewModel<ReservationsViewModel>>()
                );
            });
        }
        private static PlannistPartsListViewModel CreatePlannistDashboardMainViewModel(IServiceProvider services, IServiceProvider databaseServices) => new(services, databaseServices);
        private static DeploysViewModel CreateDeploysViewModel(IServiceProvider services, IServiceProvider databaseServices) => new(services, databaseServices);
        private static PartsOrdersViewModel CreateOrdersViewModel(IServiceProvider services, IServiceProvider databaseServices) => new(services, databaseServices);
        private static OffersViewModel CreateOffersViewModel(IServiceProvider services, IServiceProvider databaseServices) => new(services, databaseServices);
        private static ReportsViewModel CreateReportsViewModel(IServiceProvider services, IServiceProvider databaseServices) => new(services, databaseServices);
        private static ReservationsViewModel CreateReservationsViewModel(IServiceProvider services, IServiceProvider databaseServices) => new(services, databaseServices);
        private static AddOfferViewModel CreateAddOfferViewModel(IServiceProvider services, IServiceProvider databaseServices, IServiceProvider userServices) => new(services, databaseServices, userServices);
    }
}