﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace Shopfloor.Services.NotificationServices
{
    internal sealed partial class NotifierServices
    {
        public static void Get(IServiceCollection services)
        {
            NotifierService notifier = SetupNotifier();
            services.AddSingleton<INotifier>(notifier);
        }
        private static NotifierService SetupNotifier()
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
        public enum ToastMessageStyles
        {
            Information,
            Warning,
            Error,
            Success
        }
    }
}