namespace Shopfloor.Services.NotificationServices
{
    internal class Notification
    {
        public string Message { get; set; } = string.Empty;
        public NotifierType Type { get; set; } = NotifierType.Success;
    }
}