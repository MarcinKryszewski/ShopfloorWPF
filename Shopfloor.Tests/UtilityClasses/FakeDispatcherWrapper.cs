using Shopfloor.Shared.Dispatchers;

namespace Shopfloor.Tests.UtilityClasses
{
    public class FakeDispatcherWrapper : IDispatcherWrapper
    {
        public Task InvokeAsync(Action callback)
        {
            callback();
            return Task.Delay(1);
        }
    }
}