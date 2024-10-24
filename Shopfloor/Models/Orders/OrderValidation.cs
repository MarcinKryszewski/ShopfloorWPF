using System;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Orders
{
    internal class OrderValidation : IModelValidation<OrderCreationModel>
    {
        public void Validate(OrderCreationModel item)
        {
            throw new NotImplementedException();
        }
    }
}