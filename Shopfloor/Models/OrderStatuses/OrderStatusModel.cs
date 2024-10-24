using System;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.OrderStatuses
{
    internal class OrderStatusModel : IModel
    {
        required public int Id { get; init; }
        required public DateTime CreationDate { get; init; }
    }
}