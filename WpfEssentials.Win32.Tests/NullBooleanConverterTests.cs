using System.Globalization;
using WpfEssentials.Win32.Converters;
using Xunit;

namespace WpfEssentials.Win32.Tests
{
    public class NullBooleanConverterTests
    {
        [Theory]
        [InlineData(null, false)]
        [InlineData("foo", true)]
        public void Convert(object input, object expectedResult)
        {
            NullBooleanConverter conv = new NullBooleanConverter();
            object result = conv.Convert(input, null, null, CultureInfo.InvariantCulture);

            Assert.Equal(expectedResult, result);
        }
    }
}
