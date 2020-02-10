using System;
using WpfEssentials.Extensions;
using Xunit;

namespace WpfEssentials.Tests
{
    public class TypeExtensionsTests
    {
        [Theory]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(TestObject), false)]
        [InlineData(typeof(TestObject2), true)]
        public void IsObservable(Type t, bool expectedResult)
        {
            Assert.Equal(expectedResult, t.IsObservable());
        }

        private class TestObject { }
        private class TestObject2 : ObservableObject { }
    }
}
