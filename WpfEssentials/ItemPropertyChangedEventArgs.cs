using System.ComponentModel;

namespace WpfEssentials
{
    /// <summary>
    /// Provides data for the <see cref="FullyObservableCollection{T}.ItemPropertyChanged"/> event.
    /// </summary>
    public class ItemPropertyChangedEventArgs : PropertyChangedEventArgs
    {
        /// <summary>
        /// The index in the collection of the item that changed state.
        /// </summary>
        public int ItemIndex { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemPropertyChangedEventArgs"/> class.
        /// </summary>
        /// <param name="itemIndex">
        /// The index in the collection of the item that changed state.
        /// </param>
        /// <param name="propertyName">
        /// The name of the property that changed state.
        /// </param>
        public ItemPropertyChangedEventArgs(int itemIndex, string propertyName)
            : base(propertyName)
        {
            ItemIndex = itemIndex;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemPropertyChangedEventArgs"/> class.
        /// </summary>
        /// <param name="itemIndex">
        /// The index in the collection of the item that changed state.
        /// </param>
        /// <param name="args">
        /// A <see cref="PropertyChangedEventArgs"/> instance for the property that
        /// changed state.
        /// </param>
        public ItemPropertyChangedEventArgs(int itemIndex, PropertyChangedEventArgs args)
            : this(itemIndex, args.PropertyName)
        { }
    }
}
