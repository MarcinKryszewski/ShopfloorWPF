using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Shopfloor.Shared.HelperFunctions
{
    public static class BatchListUpdater
    {
        private const int _defaultBatchSize = 10;
        private const int _minimumBatchSize = 1;

        public static async Task UpdateAsync<T>(IEnumerable<T> data, List<T> privateList, ICollectionView publicList, int batchSize = _defaultBatchSize)
        {
            if (batchSize < _minimumBatchSize)
            {
                batchSize = _defaultBatchSize;
            }

            for (int i = 0; i < data.Count(); i += batchSize)
            {
                privateList.AddRange(data.Skip(i).Take(batchSize));
                await Dispatcher.CurrentDispatcher.InvokeAsync(publicList.Refresh);
            }
        }
    }
}