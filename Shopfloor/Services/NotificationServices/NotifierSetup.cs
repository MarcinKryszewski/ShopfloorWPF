using System;
using System.Windows;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using static Shopfloor.Services.NotificationServices.NotifierServices;

namespace Shopfloor.Services.NotificationServices
{
    internal partial class NotifierSetup
    {
        public static NotifierService GetSetup()
        {
            return new NotifierService(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.BottomRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }
    }
}