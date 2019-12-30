using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace WpfEssentials
{
    /// <summary>
    /// An extension of <see cref="ObservableCollection{T}"/> that allows the
    /// view to monitor changes to the state of each item in the collection,
    /// in addition to changes in the collection itself.
    /// </summary>
    /// <remarks>
    /// Adapted from code by various authors.
    /// http://code.i-harness.com/en/q/15c80f.
    /// </remarks>
    public class FullyObservableCollection<T> : ObservableCollection<T>
        where T : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property within an item in the collection changes state.
        /// </summary>
        public event EventHandler<ItemPropertyChangedEventArgs> ItemPropertyChanged;

        /// <summary>
        /// Creates a new empty <see cref="FullyObservableCollection{T}"/>.
        /// </summary>
        public FullyObservableCollection()
            : base()
        { }

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
            ObserveAll();
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (T item in e.OldItems)
                {
                    item.PropertyChanged -= ChildPropertyChanged;
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Add ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (T item in e.NewItems)
                {
                    item.PropertyChanged += ChildPropertyChanged;
                }
            }

            base.OnCollectionChanged(e);
        }

        protected void OnItemPropertyChanged(ItemPropertyChangedEventArgs e)
        {
            ItemPropertyChanged?.Invoke(this, e);
        }

        protected void OnItemPropertyChanged(int index, PropertyChangedEventArgs e)
        {
            OnItemPropertyChanged(new ItemPropertyChangedEventArgs(index, e));
        }

        protected override void ClearItems()
        {
            foreach (T item in Items)
            {
                item.PropertyChanged -= ChildPropertyChanged;
            }

            base.ClearItems();
        }

        private void ObserveAll()
        {
            foreach (T item in Items)
            {
                item.PropertyChanged += ChildPropertyChanged;
            }
        }

        private void ChildPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(sender is T typedSender))
            {
                return;
            }

            int i = Items.IndexOf(typedSender);
            if (i == -1)
            {
                return;
            }

            OnItemPropertyChanged(i, e);
        }
    }
}
