using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Shared.HelperFunctions
{
    internal static class BatchListUpdater
    {
        private const int _defaultBatchSize = 5;
        private const int _minimumBatchSize = 1;
        private const long _refreshRate = 250;
        public static event EventHandler? DataChanged;
        public static async Task UpdateAsync<T>(
            IEnumerable<T> data,
            List<T> privateList,
            int batchSize = _defaultBatchSize)
        {
            if (batchSize < _minimumBatchSize)
            {
                batchSize = _defaultBatchSize;
            }

            int dataCount = data.Count();
            await Populatelist(dataCount, batchSize, privateList, data);
        }
        private static Task Populatelist<T>(int dataCount, int batchSize, List<T> privateList, IEnumerable<T> data)
        {
            long runTime = Stopwatch.GetTimestamp();
            for (int i = 0; i <= dataCount; i += batchSize)
            {
                privateList.AddRange(data.Skip(i).Take(batchSize));
                if (Stopwatch.GetTimestamp() - runTime > _refreshRate * 10000)
                {
                    DataChanged?.Invoke(null, EventArgs.Empty);
                    runTime = Stopwatch.GetTimestamp();
                }
            }
            DataChanged?.Invoke(null, EventArgs.Empty);
            return Task.CompletedTask;
        }
    }
}