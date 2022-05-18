// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="">
//   
// </copyright>
// <summary>
//   The extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Xamariners.Core.Configuration
{
    using System;

    /// <summary>
    ///     The extensions.
    /// </summary>
    public static class Extensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The as boolean.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool AsBoolean(this string value)
        {
            return bool.Parse(value);
        }

        /// <summary>
        /// The as date time.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public static DateTime AsDateTime(this string value)
        {
            return DateTime.Parse(value);
        }

        /// <summary>
        /// The as double.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public static double AsDouble(this string value)
        {
            return double.Parse(value);
        }

        /// <summary>
        /// The as enum.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T AsEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// The as float.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public static float AsFloat(this string value)
        {
            return float.Parse(value);
        }

        /// <summary>
        /// The as guid.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="Guid"/>.
        /// </returns>
        public static Guid AsGuid(this string value)
        {
            return new Guid(value);
        }

        /// <summary>
        /// The as int 32.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int AsInt32(this string value)
        {
            return int.Parse(value);
        }

        #endregion
    }
}