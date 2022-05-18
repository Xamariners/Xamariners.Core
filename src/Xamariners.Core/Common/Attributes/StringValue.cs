// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringValue.cs" company="Xamariners ">
//     All code copyright Xamariners . all rights reserved
//     Date: 24 01 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Xamariners.Core.Common.Attributes
{
    /// <summary>
    ///     The string value.
    /// </summary>
    public class StringValue : Attribute
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StringValue"/> class.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public StringValue(string value)
        {
            Value = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the value.
        /// </summary>
        public string Value { get; private set; }

        #endregion
    }

    /// <summary>
    ///     The short string value.
    /// </summary>
    public class ShortStringValue : Attribute
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortStringValue"/> class.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public ShortStringValue(string value)
        {
            Value = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the value.
        /// </summary>
        public string Value { get; private set; }

        #endregion
    }
}