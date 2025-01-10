using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Shopfloor.Utilities.Collections
{
    public interface IReadOnlyObservableCollection<T> : IReadOnlyList<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
    }
}