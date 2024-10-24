using System;

namespace Shopfloor.Roots
{
    internal class OrdersListRoot : IRoot
    {
        public event EventHandler? DataChanged;
        public OrdersListRoot()
        {

        }
    }
}