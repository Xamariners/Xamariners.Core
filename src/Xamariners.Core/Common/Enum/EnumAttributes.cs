// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumAttributes.cs" company="Xamariners ">
//     All code copyright Xamariners . all rights reserved
//     Date: 24 01 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Xamariners.Core.Common.Enum
{
    /// <summary>
    /// The enabled attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class EnabledAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnabledAttribute"/> class.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public EnabledAttribute(bool value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether value.
        /// </summary>
        public bool Value { get; set; }
    }

    /// <summary>
    /// The order attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class OrderAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderAttribute"/> class.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public OrderAttribute(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public int Value { get; set; }
    }
}
