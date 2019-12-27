using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace WpfEssentials.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets an <see cref="Attribute"/> attached to an enum value.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="Attribute"/> to get.
        /// </typeparam>
        /// <param name="e">
        /// The enum value to get the attribute from.
        /// </param>
        /// <returns>
        /// The <see cref="Attribute"/> instance attached to the enum value,
        /// or null if none exist.
        /// </returns>
        public static T GetAttribute<T>(this Enum e) where T : Attribute
        {
            if (e == null)
            {
                return null;
            }

            MemberInfo[] m = e.GetType().GetMember(e.ToString());
            if (m.Count() == 0)
            {
                return null;
            }

            return (T) Attribute.GetCustomAttribute(m[0], typeof(T));
        }

        /// <summary>
        /// Checks whether an enum value has a specific <see cref="Attribute"/>
        /// attached to it.
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="Attribute"/> to to check for.
        /// </typeparam>
        /// <param name="e">
        /// The enum value to check for attribute presence.
        /// </param>
        /// <returns>
        /// True if the specified enum value has the attribute attached to it,
        /// false otherwise.
        /// </returns>
        public static bool HasAttribute<T>(this Enum e) where T : Attribute
        {
            if (e == null)
            {
                return false;
            }

            MemberInfo[] m = e.GetType().GetMember(e.ToString());
            if (m.Count() == 0)
            {
                return false;
            }

            return Attribute.IsDefined(m[0], typeof(T));
        }

        /// <summary>
        /// Gets the <see cref="DescriptionAttribute"/> value if present,
        /// othwerise the value's ToString() is returned.
        /// </summary>
        /// <param name="value">
        /// The enum value to get the description of.
        /// </param>
        /// <returns>
        /// The <see cref="DescriptionAttribute"/> value if present,
        /// ToString() value otherwise.
        /// </returns>
        public static string GetDescription(this Enum value)
        {
            DescriptionAttribute descAttr = GetAttribute<DescriptionAttribute>(value);
            if (descAttr == null)
            {
                return value.ToString();
            }

            return descAttr.Description;
        }
    }
}
