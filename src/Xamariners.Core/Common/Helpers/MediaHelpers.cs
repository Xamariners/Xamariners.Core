// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaHelpers.cs" company="">
//   
// </copyright>
// <summary>
//   The image helpers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Xamariners.Core.Common.Enum;
using Xamariners.Core.Model;
using Xamariners.Core.Model.Enums;

namespace Xamariners.Core.Common.Helpers
{
    using System;

    /// <summary>
    ///     The image helpers.
    /// </summary>
    public static class MediaHelpers
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get image path.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="parentObjectType">
        /// The object type.
        /// </param>
        /// <param name="parentObjectId">
        /// The container id.
        /// </param>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="extension">
        /// The extension.
        /// </param>
        /// <param name="separator">
        /// The separator.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetMediaFilePath(
            Guid id, 
            ObjectType parentObjectType, 
            Guid? parentObjectId, 
            MediaContainer container, 
            string extension = "", 
            string separator = @"\")
        {
            if (string.IsNullOrEmpty(extension))
            {
                extension = "png";
            }

            string result = string.Format(
                @"Image{5}{0}{5}{1}{5}{2}{5}{3}.{4}",
                parentObjectType,
                parentObjectId, 
                container, 
                id, 
                extension, 
                separator);

            return result;
        }

        #endregion
    }
}