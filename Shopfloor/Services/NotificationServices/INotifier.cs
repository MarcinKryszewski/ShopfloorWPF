namespace Shopfloor.Services.NotificationServices
{
    internal interface INotifier
    {
        public void Show(Notification notification);
        public void Show(string message, NotifierType type = NotifierType.Success);
        public void ShowSuccess(string message);
        public void ShowWarning(string message);
        public void ShowError(string message);
        public void ShowInformation(string message);
    }
}