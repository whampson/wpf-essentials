using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;

namespace WpfEssentials.Win32.Extensions
{
    /// <summary>
    /// A <see cref="MarkupExtension"/> that creates a collection of all values in an <see cref="Enum"/>.
    /// </summary>
    public class EnumValueCollectionExtension : MarkupExtension
    {
        /// <summary>
        /// The type of <see cref="Enum"/>.
        /// </summary>
        public Type EnumType { get; set; }

        /// <summary>
        /// The index of the first enum value to include in the collection.
        /// </summary>
        /// <remarks>
        /// Note: the index refers to the ordinal position in the list of enum values,
        /// as returned by a function such as <see cref="Enum.GetValues(Type)"/>.
        /// </remarks>
        public int StartIndex { get; set; }

        /// <summary>
        /// The index of the last enum value to include in the collection.
        /// </summary>
        /// <remarks>
        /// Note: the index refers to the ordinal position in the list of enum values,
        /// as returned by a function such as <see cref="Enum.GetValues(Type)"/>.
        /// </remarks>
        public int EndIndex { get; set; }

        /// <summary>
        /// Creates a new <see cref="EnumValueCollectionExtension"/> instance.
        /// </summary>
        public EnumValueCollectionExtension()
        {
            StartIndex = -1;
            EndIndex = -1;
        }

        /// <summary>
        /// Returns an object that is provided as the value of the target property for this markup extension.
        /// </summary>
        /// <param name="_">(unused)</param>
        /// <returns>The object value to set on the property where the extension is applied.</returns>
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
            if (StartIndex >= 0 && EndIndex >= 0 && StartIndex > EndIndex)
            {
                throw new ArgumentOutOfRangeException("StartIndex cannot be greater than EndIndex.");
            }
            
            string[] names = Enum.GetNames(enumType);
            if (StartIndex < 0) StartIndex = 0;
            if (EndIndex < 0) EndIndex = names.Length;
            
            return names
                .Select(name => Enum.Parse(enumType, name))
                .Skip(StartIndex)
                .Take(EndIndex)
                .ToList();
        }
    }
}
