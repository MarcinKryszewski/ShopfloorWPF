using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Shopfloor.Shared.Dispatchers
{
    internal class DispatcherWrapper : IDispatcherWrapper
    {
        private readonly Dispatcher _dispatcher;

        public DispatcherWrapper(Dispatcher dispatcher)
        {
            ArgumentNullException.ThrowIfNull(dispatcher);
            _dispatcher = dispatcher;
        }
        public Task InvokeAsync(Action callback)
        {
            return _dispatcher.InvokeAsync(callback).Task;
        }
    }
}