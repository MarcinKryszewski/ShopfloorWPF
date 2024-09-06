using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Shopfloor.Shared.Dispatchers;

namespace Shopfloor.Shared.HelperFunctions
{
    internal static class BatchListUpdater
    {
        private const int _defaultBatchSize = 10;
        private const int _minimumBatchSize = 1;

        public static async Task UpdateAsync<T>(IEnumerable<T> data, List<T> privateList, ICollectionView publicList, IDispatcherWrapper? dispatcher = null, int batchSize = _defaultBatchSize)
        {
            if (batchSize < _minimumBatchSize)
            {
                batchSize = _defaultBatchSize;
            }

            dispatcher ??= new DispatcherWrapper(Application.Current.Dispatcher);

            for (int i = 0; i < data.Count(); i += batchSize)
            {
                privateList.AddRange(data.Skip(i).Take(batchSize));
                await dispatcher.InvokeAsync(publicList.Refresh);
            }
        }
    }
}