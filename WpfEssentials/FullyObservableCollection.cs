using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using WpfEssentials.Extensions;

namespace WpfEssentials
{
    /// <summary>
    /// An extension of <see cref="ObservableCollection{T}"/> that allows the
    /// view to monitor changes to the state of each item in the collection,
    /// in addition to monitoring changes made to the collection itself.
    /// </summary>
    /// <remarks>
    /// Adopted and exapanded from code by various authors.
    /// http://code.i-harness.com/en/q/15c80f.
    /// </remarks>
    public class FullyObservableCollection<T> : ObservableCollection<T>, INotifyItemStateChanged
    {
        /// <summary>
        /// Occurs when a property within an item in the collection changes state.
        /// Note: this is only fired if the item type implements <see cref="INotifyPropertyChanged"/>.
        /// </summary>
        public event NotifyItemStateChangedEventHandler ItemStateChanged;

        private readonly bool m_itemsAreObservable;

        /// <summary>
        /// Creates a new empty <see cref="FullyObservableCollection{T}"/>.
        /// </summary>
        public FullyObservableCollection()
            : base()
        {
            m_itemsAreObservable = typeof(T).IsObservable();
        }

        /// <summary>
        /// Creates a new <see cref="FullyObservableCollection{T}"/>
        /// containing items copied from the specified list.
        /// </summary>
        /// <param name="list">
        /// A list containing items that the <see cref="FullyObservableCollection{T}"/>
        /// will be initialized with.
        /// </param>
        public FullyObservableCollection(List<T> list)
            : base(list)
        {
            m_itemsAreObservable = typeof(T).IsObservable();
            ObserveAll();
        }

        /// <summary>
        /// Creates a new <see cref="FullyObservableCollection{T}"/>
        /// containing items copied from the specified collection.
        /// </summary>
        /// <param name="collection">
        /// A collection containing items that the <see cref="FullyObservableCollection{T}"/>
        /// will be initialized with.
        /// </param>
        public FullyObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
            m_itemsAreObservable = typeof(T).IsObservable();
            ObserveAll();
        }

        /// <summary>
        /// Registers or unregisters <see cref="INotifyPropertyChanged"/> event handlers from
        /// items in the list depending on <paramref name="e"/>, then fires the
        /// <see cref="ObservableCollection{T}.CollectionChanged"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (m_itemsAreObservable)
            {
                if (e.Action == NotifyCollectionChangedAction.Remove ||
                    e.Action == NotifyCollectionChangedAction.Replace)
                {
                    foreach (T item in e.OldItems)
                    {
                        ((INotifyPropertyChanged) item).PropertyChanged -= ItemPropertyChangedHandler;
                    }
                }

                if (e.Action == NotifyCollectionChangedAction.Add ||
                    e.Action == NotifyCollectionChangedAction.Replace)
                {
                    foreach (T item in e.NewItems)
                    {
                        ((INotifyPropertyChanged) item).PropertyChanged += ItemPropertyChangedHandler;
                    }
                }
            }

            base.OnCollectionChanged(e);
        }

        /// <summary>
        /// Unregisters <see cref="INotifyPropertyChanged"/> event handlers from each item,
        /// then empties the collection.
        /// </summary>
        protected override void ClearItems()
        {
            if (m_itemsAreObservable)
            {
                foreach (T item in Items)
                {
                    ((INotifyPropertyChanged) item).PropertyChanged -= ItemPropertyChangedHandler;
                }
            }

            base.ClearItems();
        }

        private void ObserveAll()
        {
            if (m_itemsAreObservable)
            {
                foreach (T item in Items)
                {
                    ((INotifyPropertyChanged) item).PropertyChanged += ItemPropertyChangedHandler;
                }
            }
        }

        private void ItemPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (sender is T typedSender)
            {
                int i = Items.IndexOf(typedSender);
                if (i > -1)
                {
                    ItemStateChanged?.Invoke(this, new ItemStateChangedEventArgs(i, e));
                }
            }
        }
    }
}
