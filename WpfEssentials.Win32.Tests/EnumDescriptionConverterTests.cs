using System.ComponentModel;
using System.Globalization;
using WpfEssentials.Win32.Converters;
using Xunit;

namespace WpfEssentials.Win32.Tests
{
    public class EnumDescriptionConverterTests
    {
        const string Description0 = "Test description 0";
        const string Description1 = nameof(TestEnum.Value1);

        public enum TestEnum
        {
            [Description(Description0)]
            Value0,
            Value1
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("foo", "foo")]
        [InlineData(TestEnum.Value0, Description0)]
        [InlineData(TestEnum.Value1, Description1)]
        public void Convert(object input, object expectedResult)
        {
            EnumDescriptionConverter conv = new EnumDescriptionConverter();
            object result = conv.Convert(input, null, null, CultureInfo.InvariantCulture);

            Assert.Equal(expectedResult, result);
        }
    }
}
