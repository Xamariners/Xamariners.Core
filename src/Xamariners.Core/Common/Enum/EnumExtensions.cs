// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The enum extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Xamariners.Core.Common.Helpers;

namespace Xamariners.Core.Common.Enum
{
    using Enum = System.Enum;

    /// <summary>
    ///     The enum extensions.
    /// </summary>
    public static class EnumExtensions
    {
        #region Public Methods and Operators


        /// <summary>
        /// Returns an enabled attribute value for specified enum value
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool GetEnabled(this System.Enum value)
        {
            MiscHelpers.ThrowIfNull(() => value);

            string enumValue = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetRuntimeField(enumValue);
            var attributes = (EnabledAttribute[])fieldInfo.GetCustomAttributes(typeof(EnabledAttribute), false);

            bool enabled = false;
            if (attributes.Length > 0)
            {
                enabled = attributes[0].Value;
            }

            return enabled;
        }

        /// <summary>
        /// Returns an order attribute value for specified enum value
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetOrder(this System.Enum value)
        {
            MiscHelpers.ThrowIfNull(() => value);

            string enumValue = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetRuntimeField(enumValue);
            var attributes = (OrderAttribute[])fieldInfo.GetCustomAttributes(typeof(OrderAttribute), false);

            int order = int.MaxValue;
            if (attributes.Length > 0)
            {
                order = attributes[0].Value;
            }

            return order;
        }

        /// <summary>
        ///     Returns collection of pair name/value for specified enum type
        /// </summary>
        /// <returns>
        ///     The <see cref="Collection" />.
        /// </returns>
        public static Collection<KeyValuePair<string, int>> ToCollection<T>()
        {
            var collection = new Collection<KeyValuePair<string, int>>();
            Array values = System.Enum.GetValues(typeof(T));
            foreach (int value in values)
            {
                collection.Add(new KeyValuePair<string, int>(System.Enum.GetName(typeof(T), value), value));
            }

            return collection;
        }

        /// <summary>
        ///     Returns collection of pair value/order for specified enum type
        /// </summary>
        /// <returns>
        ///     The <see cref="IOrderedEnumerable" />.
        /// </returns>
        public static IOrderedEnumerable<KeyValuePair<int, int>> ToOrderedCollection<T>()
        {
            var collection = new Collection<KeyValuePair<int, int>>();
            Array values = System.Enum.GetValues(typeof(T));
            foreach (System.Enum value in values)
            {
                collection.Add(new KeyValuePair<int, int>(Convert.ToInt32(value), GetOrder(value)));
            }

            return collection.OrderBy(i => i.Value);
        }

      

        //public static string GetDescriptionFromEnumValue(Enum value)
        //{
        //     DisplayAttribute attribute = value.GetType()
        //        .GetField(value.ToString())
        //        .GetCustomAttributes(typeof(DisplayAttribute), false)
        //        .SingleOrDefault() as DisplayAttribute;
        //    return attribute == null ? value.ToString() : attribute.Description;
        //}

        #endregion
    }
}