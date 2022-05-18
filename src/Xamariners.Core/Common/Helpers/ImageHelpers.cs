// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageHelpers.cs" company="Xamariners ">
//     All code copyright Xamariners . all rights reserved
//     Date: 24 01 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Xamariners.Core.Common.Enum;
using Xamariners.Core.Model.Enums;

namespace Xamariners.Core.Model
{
    /// <summary>
    /// The image helpers.
    /// </summary>
    public static partial class ImageHelpers
    {
        public const float MAX_RATIO_PICTURE = 1.8f;

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
        public static string GetImagePath(
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


        public static bool IsPanoramicImage(int height, int width)
        {
            var smallest = Math.Min(width, height);
            var largest = Math.Max(width, height);

            var ratio = largest / smallest;

            if (ratio > MAX_RATIO_PICTURE)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}