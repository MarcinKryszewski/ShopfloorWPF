namespace Shopfloor.Services.NotificationServices
{
    internal interface INotifier
    {
        public void ShowSuccess(string message);
        public void ShowError(string message);
        public void ShowInformation(string message);
    }
}