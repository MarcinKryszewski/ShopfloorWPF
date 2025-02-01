using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using System.Windows.Threading;

namespace Shopfloor.Utilities.Collections.Internals
{
    [ExcludeFromCodeCoverage]
    internal sealed class DispatchedObservableCollection<T> : ObservableCollectionBase<T>, IReadOnlyObservableCollection<T>, IList<T>, IList
    {
        private readonly ConcurrentObservableCollection<T> _collection;
        private readonly Dispatcher _dispatcher;
        private readonly ConcurrentQueue<PendingEvent<T>> _pendingEvents = new();
        private bool _isDispatcherPending;

        public DispatchedObservableCollection(ConcurrentObservableCollection<T> collection, Dispatcher dispatcher)
            : base(collection)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }

        public int Count
        {
            get
            {
                AssertIsOnDispatcherThread();
                return Items.Count;
            }
        }
        int ICollection.Count
        {
            get
            {
                AssertIsOnDispatcherThread();
                return Count;
            }
        }
        bool IList.IsFixedSize
        {
            get
            {
                AssertIsOnDispatcherThread();
                return ((IList)Items).IsFixedSize;
            }
        }
        bool ICollection<T>.IsReadOnly
        {
            get
            {
                AssertIsOnDispatcherThread();
                return ((ICollection<T>)_collection).IsReadOnly;
            }
        }
        bool IList.IsReadOnly
        {
            get
            {
                AssertIsOnDispatcherThread();
                return ((IList)Items).IsReadOnly;
            }
        }
        bool ICollection.IsSynchronized
        {
            get
            {
                AssertIsOnDispatcherThread();
                return ((ICollection)Items).IsSynchronized;
            }
        }
        object ICollection.SyncRoot
        {
            get
            {
                AssertIsOnDispatcherThread();
                return ((ICollection)Items).SyncRoot;
            }
        }
        object? IList.this[int index]
        {
            get
            {
                AssertIsOnDispatcherThread();
                return this[index];
            }

            set
            {
                // it will immediatly modify both collections as we are on the dispatcher thread
                AssertType(value, nameof(value));
                AssertIsOnDispatcherThread();
                _collection[index] = (T)value!;
            }
        }
        T IList<T>.this[int index]
        {
            get
            {
                AssertIsOnDispatcherThread();
                return this[index];
            }
            set
            {
                // it will immediatly modify both collections as we are on the dispatcher thread
                AssertIsOnDispatcherThread();
                _collection[index] = value;
            }
        }
        public T this[int index]
        {
            get
            {
                AssertIsOnDispatcherThread();
                return Items[index];
            }
        }
        void ICollection<T>.Add(T item)
        {
            // it will immediatly modify both collections as we are on the dispatcher thread
            AssertIsOnDispatcherThread();
            _collection.Add(item);
        }
        int IList.Add(object? value)
        {
            // it will immediatly modify both collections as we are on the dispatcher thread
            AssertType(value, nameof(value));
            AssertIsOnDispatcherThread();
            return ((IList)_collection).Add(value);
        }
        void ICollection<T>.Clear()
        {
            // it will immediatly modify both collections as we are on the dispatcher thread
            AssertIsOnDispatcherThread();
            _collection.Clear();
        }
        void IList.Clear()
        {
            // it will immediatly modify both collections as we are on the dispatcher thread
            AssertIsOnDispatcherThread();
            ((IList)_collection).Clear();
        }
        public bool Contains(T item)
        {
            AssertIsOnDispatcherThread();
            return Items.Contains(item);
        }
        bool IList.Contains(object? value)
        {
            // it will immediatly modify both collections as we are on the dispatcher thread
            AssertType(value, nameof(value));
            AssertIsOnDispatcherThread();
            return ((IList)_collection).Contains(value);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            AssertIsOnDispatcherThread();
            Items.CopyTo(array, arrayIndex);
        }
        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)Items).CopyTo(array, index);
        }
        public IEnumerator<T> GetEnumerator()
        {
            AssertIsOnDispatcherThread();
            return Items.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public int IndexOf(T item)
        {
            AssertIsOnDispatcherThread();
            return Items.IndexOf(item);
        }
        int IList.IndexOf(object? value)
        {
            // it will immediatly modify both collections as we are on the dispatcher thread
            AssertType(value, nameof(value));
            AssertIsOnDispatcherThread();
            return Items.IndexOf((T)value!);
        }
        void IList<T>.Insert(int index, T item)
        {
            // it will immediatly modify both collections as we are on the dispatcher thread
            AssertIsOnDispatcherThread();
            _collection.Insert(index, item);
        }
        void IList.Insert(int index, object? value)
        {
            // it will immediatly modify both collections as we are on the dispatcher thread
            AssertType(value, nameof(value));
            AssertIsOnDispatcherThread();
            ((IList)_collection).Insert(index, value);
        }
        bool ICollection<T>.Remove(T item)
        {
            // it will immediatly modify both collections as we are on the dispatcher thread
            AssertIsOnDispatcherThread();
            return _collection.Remove(item);
        }
        void IList.Remove(object? value)
        {
            // it will immediatly modify both collections as we are on the dispatcher thread
            AssertType(value, nameof(value));
            AssertIsOnDispatcherThread();
            ((IList)_collection).Remove(value);
        }
        void IList<T>.RemoveAt(int index)
        {
            // it will immediatly modify both collections as we are on the dispatcher thread
            AssertIsOnDispatcherThread();
            _collection.RemoveAt(index);
        }
        void IList.RemoveAt(int index)
        {
            // it will immediatly modify both collections as we are on the dispatcher thread
            AssertIsOnDispatcherThread();
            ((IList)_collection).RemoveAt(index);
        }
        internal void EnqueueAdd(T item)
        {
            EnqueueEvent(PendingEvent.Add(item));
        }
        internal void EnqueueAddRange(System.Collections.Immutable.ImmutableList<T> items)
        {
            EnqueueEvent(PendingEvent.AddRange(items));
        }
        internal void EnqueueClear()
        {
            EnqueueEvent(PendingEvent.Clear<T>());
        }
        internal void EnqueueInsert(int index, T item)
        {
            EnqueueEvent(PendingEvent.Insert(index, item));
        }
        internal void EnqueueInsertRange(int index, System.Collections.Immutable.ImmutableList<T> items)
        {
            EnqueueEvent(PendingEvent.InsertRange(index, items));
        }
        internal bool EnqueueRemove(T item)
        {
            EnqueueEvent(PendingEvent.Remove(item));
            return true;
        }
        internal void EnqueueRemoveAt(int index)
        {
            EnqueueEvent(PendingEvent.RemoveAt<T>(index));
        }
        internal void EnqueueReplace(int index, T value)
        {
            EnqueueEvent(PendingEvent.Replace(index, value));
        }
        internal void EnqueueReset(System.Collections.Immutable.ImmutableList<T> items)
        {
            EnqueueEvent(PendingEvent.Reset(items));
        }
        private static void AssertType(object? value, string argumentName)
        {
            if (value is null || value is T)
            {
                return;
            }

            throw new ArgumentException($"value must be of type '{typeof(T).FullName}'", argumentName);
        }
        private void AssertIsOnDispatcherThread()
        {
            if (!IsOnDispatcherThread())
            {
                var currentThreadId = Environment.CurrentManagedThreadId;
                throw new InvalidOperationException("The collection must be accessed from the dispatcher thread only. Current thread ID: " + currentThreadId.ToString(CultureInfo.InvariantCulture));
            }
        }
        private void EnqueueEvent(PendingEvent<T> @event)
        {
            _pendingEvents.Enqueue(@event);
            ProcessPendingEventsOrDispatch();
        }

        private bool IsOnDispatcherThread()
        {
            return _dispatcher.Thread == Thread.CurrentThread;
        }
        private void ProcessPendingEvents()
        {
            _isDispatcherPending = false;
            while (_pendingEvents.TryDequeue(out var pendingEvent))
            {
                switch (pendingEvent.Type)
                {
                    case PendingEventType.Add:
                        AddItem(pendingEvent.Item);
                        break;

                    case PendingEventType.AddRange:
                        if (pendingEvent.Items is null)
                        {
                            break;
                        }
                        AddItems(pendingEvent.Items);
                        break;

                    case PendingEventType.Remove:
                        RemoveItem(pendingEvent.Item);
                        break;

                    case PendingEventType.Clear:
                        ClearItems();
                        break;

                    case PendingEventType.Insert:
                        InsertItem(pendingEvent.Index, pendingEvent.Item);
                        break;

                    case PendingEventType.InsertRange:
                        if (pendingEvent.Items is null)
                        {
                            break;
                        }
                        InsertItems(pendingEvent.Index, pendingEvent.Items);
                        break;

                    case PendingEventType.RemoveAt:
                        RemoveItemAt(pendingEvent.Index);
                        break;

                    case PendingEventType.Replace:
                        ReplaceItem(pendingEvent.Index, pendingEvent.Item);
                        break;

                    case PendingEventType.Reset:
                        if (pendingEvent.Items is null)
                        {
                            break;
                        }
                        Reset(pendingEvent.Items);
                        break;
                }
            }
        }
        private void ProcessPendingEventsOrDispatch()
        {
            if (!IsOnDispatcherThread())
            {
                if (!_isDispatcherPending)
                {
                    _isDispatcherPending = true;
                    _ = _dispatcher.BeginInvoke(ProcessPendingEvents);
                }

                return;
            }

            ProcessPendingEvents();
        }
    }
}