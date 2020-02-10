using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using WpfEssentials.Extensions;
using WpfEssentials.Win32.Properties;

namespace WpfEssentials.Win32.Converters
{
    /// <summary>
    /// Converts an <see cref="Enum"/> value into a <see cref="string"/> containing the
    /// text from it's <see cref="DescriptionAttribute"/>. If no DescriptionAttribute is present, the
    /// Enum's ToString() value is used instead.
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(string))]
    public sealed class EnumDescriptionConverter : IValueConverter
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is Enum))
            {
                return value;
            }

            return (value as Enum).GetDescription();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(Resources.NotSupported_ConvertBack); ;
        }
#pragma warning restore CS1591
    }
}
