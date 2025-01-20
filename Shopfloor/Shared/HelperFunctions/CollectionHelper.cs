using System.Collections;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Shared.HelperFunctions
{
    internal static class CollectionHelper
    {
        public static bool IsInIEnumerable<T>(string name, IEnumerable table)
        where T : IModel
        {
            foreach (T item in table)
            {
                if (item.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}