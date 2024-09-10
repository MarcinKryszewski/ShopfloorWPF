using System;

namespace Shopfloor.UnitOfWorks
{
    internal interface IUnitOfWork
    {
        public event EventHandler? DecoratingCompleted;
    }
}