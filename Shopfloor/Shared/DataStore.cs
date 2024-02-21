using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Interfaces;

namespace Shopfloor.Shared
{
    internal static class DataStore
    {
        public static async Task<List<T>> GetData<T>(IDataStore<T> dataStore)
        {
            if (!dataStore.IsLoaded) await dataStore.Load();
            return dataStore.Data;
        }
        public static async Task LoadData<T>(IDataStore<T> dataStore)
        {
            if (!dataStore.IsLoaded) await dataStore.Load();
        }
    }
}