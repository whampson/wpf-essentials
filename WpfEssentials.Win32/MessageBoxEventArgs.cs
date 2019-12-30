using System;
using System.Windows;

namespace WpfEssentials.Win32
{
    /// <summary>
    /// Provides data for invoking a message box via an event.
    /// </summary>
    public class MessageBoxEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the text to display in the body of the message box.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the text to dsiplay in the window title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="MessageBoxButton"/> value that specifies which button or buttons to display.
        /// </summary>
        public MessageBoxButton Buttons { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="MessageBoxImage"/> value that specifies the icon to display.
        /// </summary>
        public MessageBoxImage Icon { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="MessageBoxResult"/> value that specifies the default result of the message box.
        /// </summary>
        public MessageBoxResult DefaultResult { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="MessageBoxOptions"/> value that specifies the options.
        /// </summary>
        public MessageBoxOptions Options { get; set; }

        /// <summary>
        /// Gets or sets the callback <see cref="Action{T}"/> that is invoked after the message box is closed.
        /// </summary>
        public Action<MessageBoxResult> Callback { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">The text to display in the body of the message box.</param>
        /// <param name="title">The text to dsiplay in the window title.</param>
        /// <param name="buttons">The <see cref="MessageBoxImage"/> value that specifies the icon to display.</param>
        /// <param name="icon">The <see cref="MessageBoxImage"/> value that specifies the icon to display.</param>
        /// <param name="defaultResult">The <see cref="MessageBoxResult"/> value that specifies the default result of the message box.</param>
        /// <param name="options">The <see cref="MessageBoxOptions"/> value that specifies the options.</param>
        /// <param name="callback">The callback <see cref="Action{T}"/> that is invoked after the message box is closed.</param>
        public MessageBoxEventArgs(string text,
            string title = "",
            MessageBoxButton buttons = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.None,
            MessageBoxResult defaultResult = MessageBoxResult.None,
            MessageBoxOptions options = MessageBoxOptions.None,
            Action<MessageBoxResult> callback = null)
        {
            Text = text;
            Title = title;
            Buttons = buttons;
            Icon = icon;
            DefaultResult = defaultResult;
            Options = options;
            Callback = callback;
        }

        /// <summary>
        /// Show a message box with the provided arguments,
        /// then performs the callback action if provided.
        /// </summary>
        public void Show()
        {
            Show(null);
        }

        /// <summary>
        /// Show a message box over the specified <see cref="Window"/> with the provided arguments,
        /// then performs the callback action if provided.
        /// <param name="owner">A <see cref="Window"/> that represents the owner window of the message box.</param>
        /// </summary>
        public void Show(Window owner)
        {
            MessageBoxResult result = (owner == null)
                ? MessageBoxEx.Show(Text, Title, Buttons, Icon, DefaultResult, Options)
                : MessageBoxEx.Show(owner, Text, Title, Buttons, Icon, DefaultResult, Options);

            Callback?.Invoke(result);
        }
    }
}
