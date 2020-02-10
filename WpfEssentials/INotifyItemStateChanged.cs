namespace WpfEssentials
{
    /// <summary>
    /// Notifies listeners of changes made to a mutable item within a collection.
    /// </summary>
    public interface INotifyItemStateChanged
    {
        /// <summary>
        /// Occurs when a property changes on a mutable item within a collection.
        /// </summary>
        event NotifyItemStateChangedEventHandler ItemStateChanged;
    }
}
