using System;
using System.Globalization;
using System.Windows.Data;
using WpfEssentials.Extensions;

namespace WpfEssentials.Converters
{
    /// <summary>
    /// Converts an Enum value into a string containing the text from it's
    /// Description attribute. If no Description Attribute is available,
    /// the Enum's ToString() value is used instead.
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(string))]
    public class EnumDescriptionConverter : IValueConverter
    {
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
            return value;
        }
    }
}
