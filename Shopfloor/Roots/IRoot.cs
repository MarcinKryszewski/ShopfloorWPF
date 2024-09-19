using System;

namespace Shopfloor.Roots
{
    internal interface IRoot
    {
        public event EventHandler? DataChanged;
    }
}