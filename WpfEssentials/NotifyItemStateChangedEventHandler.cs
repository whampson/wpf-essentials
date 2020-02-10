namespace WpfEssentials
{
    /// <summary>
    /// Represents the method that handles the <see cref="INotifyItemStateChanged.ItemStateChanged"/> event.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">Information about the event.</param>
    public delegate void NotifyItemStateChangedEventHandler(object sender, ItemStateChangedEventArgs e);
}
