using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfEssentials.Win32.Converters
{
    /// <summary>
    /// Gets a true/false value indicating whether a value is null.
    /// </summary>
    public class NullBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported for this converter.");
        }
    }
}
