using System.Globalization;
using System.Windows;
using WpfEssentials.Win32.Converters;
using Xunit;

namespace WpfEssentials.Win32.Tests
{
    public class InvertBooleanConverterTests
    {
        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData("foo", null)]
        public void Convert(object input, object expectedResult)
        {
            InvertBooleanConverter conv = new InvertBooleanConverter();
            object result = conv.Convert(input, null, null, CultureInfo.InvariantCulture);

            if (result == DependencyProperty.UnsetValue)
            {
                // 'null' functions as a sentinel for DependencyProperty.UnsetValue since we can't put that in InlineData
                result = null;
            }

            Assert.Equal(expectedResult, result);
        }
    }
}
