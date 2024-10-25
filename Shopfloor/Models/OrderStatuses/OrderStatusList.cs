namespace Shopfloor.Models.OrderStatuses
{
    internal static class OrderStatusList
    {
        public enum StatusList
        {
            New,
            Approving,
            Approved,
            Rejected,
            Offers,
            Order,
            InTransit,
            OnStock,
            Delivered,
            Completed,
            Cancelled,
        }
    }
}