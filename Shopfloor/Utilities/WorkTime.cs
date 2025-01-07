using System.Diagnostics;

namespace Shopfloor.Utilities
{
    internal class WorkTime
    {
        private readonly long _startTime = Stopwatch.GetTimestamp();
        public void HowLong()
        {
            Debug.WriteLine("Elapsed time " + ((Stopwatch.GetTimestamp() - _startTime) / 10000));
        }
    }
}
