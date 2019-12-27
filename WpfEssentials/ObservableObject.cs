using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WpfEssentials
{
    /// <summary>
    /// Allows changes in object state to be detected by the view
    /// via the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <remarks>
    /// Adapted from Rachel Lim's ObservableObject class.
    /// https://rachel53461.wordpress.com/2011/05/08/simplemvvmexample/
    /// </remarks>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed state.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            VerifyPropertyName(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Warns the developer if this object does not have a public property
        /// with the specified name. This method does not exist in a Release build.
        /// </summary>
        /// <param name="propertyName">The property name to check.</param>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        protected virtual void VerifyPropertyName(string propertyName)
        {
            string msg = "Invalid property name: " + propertyName;

            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                if (ThrowOnInvalidPropertyName)
                {
                    throw new Exception(msg);
                }
                else
                {
                    Debug.Fail(msg);
                }
            }
        }

        /// <summary>
        /// Returns whether an exception is thrown, or if a Debug.Fail() is used
        /// when an invalid property name is passed to the VerifyPropertyName method.
        /// The default value is false, but subclasses used by unit tests might
        /// override this property's getter to return true.
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }
    }
}
