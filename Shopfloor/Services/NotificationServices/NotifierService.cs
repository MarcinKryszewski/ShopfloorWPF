using System;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Messages.Error;
using ToastNotifications.Messages.Information;
using ToastNotifications.Messages.Success;
using ToastNotifications.Messages.Warning;

namespace Shopfloor.Services.NotificationServices
{
    internal sealed partial class NotifierServices
    {
        internal sealed class NotifierService : Notifier, INotifier
        {
            public NotifierService(Action<NotifierConfiguration> configureAction)
                : base(configureAction)
            {
            }
            public void ShowError(string message)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Notify(() => new ErrorMessage(message));
                });
            }
            public void ShowInformation(string message)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Notify(() => new InformationMessage(message));
                });
            }
            public void ShowSuccess(string message)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Notify(() => new SuccessMessage(message));
                });
            }
            public void ShowWarning(string message)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Notify(() => new WarningMessage(message));
                });
            }
        }
    }
}