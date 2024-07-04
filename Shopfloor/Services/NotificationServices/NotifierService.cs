using System;
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
                Notify(() => new ErrorMessage(message));
            }
            public void ShowInformation(string message)
            {
                Notify(() => new InformationMessage(message));
            }
            public void ShowSuccess(string message)
            {
                Notify(() => new SuccessMessage(message));
            }
            public void ShowWarning(string message)
            {
                Notify(() => new WarningMessage(message));
            }
        }
    }
}