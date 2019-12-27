using System;
using System.ComponentModel;
using WpfEssentials.Extensions;
using Xunit;

namespace WpfEssentials.Tests
{
    public class EnumExtensionsTests
    {
        const string Description0 = "Test description 0";

        public enum TestEnum
        {
            [Description(Description0)]
            Value0,
            Value1
        }

        [Fact]
        public void HasAttribute()
        {
            bool x0 = TestEnum.Value0.HasAttribute<DescriptionAttribute>();
            bool x1 = TestEnum.Value1.HasAttribute<DescriptionAttribute>();

            Assert.True(x0);
            Assert.False(x1);
        }

        [Fact]
        public void GetAttribute()
        {
            Attribute attr0 = TestEnum.Value0.GetAttribute<DescriptionAttribute>();
            Attribute attr1 = TestEnum.Value1.GetAttribute<DescriptionAttribute>();

            Assert.NotNull(attr0);
            Assert.Null(attr1);
        }

        [Fact]
        public void GetDescription()
        {
            TestEnum e0 = TestEnum.Value0;
            TestEnum e1 = TestEnum.Value1;

            string desc0 = e0.GetDescription();
            string desc1 = e1.GetDescription();

            Assert.Equal(Description0, desc0);
            Assert.Equal(e1.ToString(), desc1);
        }
    }
}
