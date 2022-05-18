// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelHelper.cs" company="">
//   
// </copyright>
// <summary>
//   The model helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Xamariners.Core.Model.Internal;

namespace Xamariners.Core.Model.Helpers
{
    using System;

    /// <summary>
    ///     The model helper.
    /// </summary>
    public static class ModelHelper
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get key.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetKey<T>(T data)
        {
            if (data is CoreObject)
            {
                return (data as CoreObject).Id.ToString();
            }

            return Guid.NewGuid().ToString();
        }

        #endregion
    }
}