using System;
using System.Threading.Tasks;

namespace Shopfloor.Shared.Dispatchers
{
    internal interface IDispatcherWrapper
    {
        Task InvokeAsync(Action callback);
    }
}