using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;

namespace WpfEssentials.Win32.Extensions
{
    public class EnumValueCollectionExtension : MarkupExtension
    {
        public Type EnumType { get; set; }

        public override object ProvideValue(IServiceProvider _)
        {
            if (EnumType != null)
            {
                return CreateEnumValueList(EnumType);
            }

            return default;
        }

        private List<object> CreateEnumValueList(Type enumType)
        {
            return Enum.GetNames(enumType)
                .Select(name => Enum.Parse(enumType, name))
                .ToList();
        }
    }
}
