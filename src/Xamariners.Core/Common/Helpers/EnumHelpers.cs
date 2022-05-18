using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamariners.Core.Common.Helpers
{
    using System.Reflection;

    using Xamariners.Core.Common.Attributes;

    using Enum = System.Enum;

    public static class EnumHelpers
    {
        /// <summary>
        /// The get enum short string value non localized.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetEnumShortStringValueNonLocalized(this Enum value)
        {
            string output = null;
            Type type = value.GetType();

            FieldInfo fi = type.GetRuntimeField(value.ToString());
            var attrs1 = fi.GetCustomAttributes(typeof(ShortStringValue), false) as ShortStringValue[];
            if (attrs1 != null && attrs1.Length > 0)
            {
                output = attrs1[0].Value;
            }
            else
            {
                var attrs2 = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
                output = attrs2 != null && attrs2.Length > 0 ? attrs2[0].Value : value.ToString();
            }

            return output;
        }

        /// <summary>
        /// The get enum string value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetEnumStringValueNotLocalized(this Enum value)
        {
            string output = null;
            Type type = value.GetType();

            FieldInfo fi = type.GetRuntimeField(value.ToString());
            var attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }
            else
            {
                output = value.ToString();
            }

            return output;
        }

        /// <summary>
        /// string array to enum array
        /// </summary>
        /// <param name="enumList"></param>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static IList<TEnum> ToEnumArray<TEnum>(string[] enumList) where TEnum : struct, IComparable, IFormattable
        {
            if (!typeof(TEnum).GetTypeInfo().IsEnum)
                throw new ArgumentException("TEnum must be an enumerated type");

            IList<TEnum> items = enumList.Select(x => (TEnum)Enum.Parse(typeof(TEnum), x)).ToList();
            return items;
        }
    }
}
