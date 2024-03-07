using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace Shopfloor.Services.NotificationServices
{
    internal sealed class NotifierService
    {
        public static void Get(IServiceCollection services)
        {
            Notifier notifier = SetupNotifier();
            services.AddSingleton(notifier);
        }
        private static Notifier SetupNotifier()
        {
            return new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.BottomRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(5),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(3));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }
        public enum ToastMessageStyles
        {
            Information,
            Warning,
            Error,
            Success
        }
    }
}