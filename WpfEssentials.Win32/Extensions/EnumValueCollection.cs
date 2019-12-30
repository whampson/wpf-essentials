using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;

namespace WpfEssentials.Win32.Extensions
{
    /// <summary>
    /// A <see cref="MarkupExtension"/> that creates a collection of all values in
    /// an <see cref="Enum"/>.
    /// </summary>
    public class EnumValueCollectionExtension : MarkupExtension
    {
        /// <summary>
        /// The type of <see cref="Enum"/>.
        /// </summary>
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
