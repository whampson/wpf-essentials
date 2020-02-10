using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfEssentials.Win32.Converters
{
    /// <summary>
    /// Inverts a <see cref="bool"/> value.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public sealed class InvertBooleanConverter : IValueConverter
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is bool)
            {
                return !((bool) value);
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
#pragma warning restore CS1591
    }
}
