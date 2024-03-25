using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Models.OrderModel.Store.Combine
{
    public class OrderCombiner : ICombiner
    {
        public Task Combine()
        {
            return Task.CompletedTask;
        }
    }
}