using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopfloor.Database;
using Shopfloor.Database.Initializers;
using Shopfloor.Features.Mechanic.MechanicDashboard;
using Shopfloor.Hosts;
using Shopfloor.Layout.MainWindow;
using Shopfloor.Models.UserModel;
using Shopfloor.Services.NavigationServices;
using Shopfloor.Stores;
using System;
using System.Data;
using System.Windows;
using ToastNotifications;

namespace Shopfloor
{
    public partial class App : Application
    {
        private readonly IHost _appHost;

        public App()
        {
            _appHost = AppHost.Get();
            _appHost.Start();
        }

        private void ApplicationStart(object sender, StartupEventArgs e)
        {
            IServiceProvider services = _appHost.Services;

            using IDbConnection connection = services.GetRequiredService<DatabaseConnectionFactory>().Connect();
            DatabaseInitializerFactory initializer = new(_appHost.Services, connection);
            IDatabaseInitializer databaseInitializer = initializer.CreateInitializer();
            databaseInitializer.Initialize();

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(services)
            };
            MainWindow.Show();
            AutoLogin(services);
            DashboardNavigate();

            //NavigationService<MechanicDashboardViewModel> navigationService = _mainHost.Services.GetRequiredService<NavigationService<MechanicDashboardViewModel>>();
            //navigationService.Navigate();
        }
        //tries to login user automatically
        private static void AutoLogin(IServiceProvider services)
        {
            CurrentUserStore loginService = services.GetRequiredService<CurrentUserStore>();
            UserProvider userProvider = services.GetRequiredService<UserProvider>();
            Notifier notifier = services.GetRequiredService<Notifier>();
            string userName = Environment.UserName;
            loginService.AutoLogin(userName, userProvider);
        }

        private void DashboardNavigate()
        {
            /*CurrentUserStore currentUser = _userHost.Services.GetRequiredService<CurrentUserStore>();
            if (currentUser.HasRole(777))
            {
                //_mainHost.Services.GetRequiredService<NavigationService<ManagerDashboardViewModel>>().Navigate();
                return;
            }
            if (currentUser.HasRole(460))
            {
                //_mainHost.Services.GetRequiredService<NavigationService<PlannistDashboardViewModel>>().Navigate();
                return;
            }
            //_mainHost.Services.GetRequiredService<NavigationService<MechanicDashboardViewModel>>().Navigate();*/
            NavigationService navigation = _appHost.Services.GetRequiredService<NavigationService>();
            navigation.NavigateTo<MechanicDashboardViewModel>();
        }
    }
}