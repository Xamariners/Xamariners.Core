// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaFileInfo.cs" company="">
//   
// </copyright>
// <summary>
//   The image.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

using Xamariners.Core.Common.Enum;
using Xamariners.Core.Model.Attributes;
using Xamariners.Core.Model.Enums;
using Xamariners.Core.Model.Internal;

namespace Xamariners.Core.Model
{
    using System.IO;

    /// <summary>
    ///     The image.
    /// </summary>
    //[PSerializable]
    public class MediaFileInfo : CoreObject, IMediaFileInfo
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets Container (main, secondary...)
        /// </summary>
        public MediaContainer Container { get; set; }

        /// <summary>
        ///     Gets or sets the extension.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        ///     Gets or sets the file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///     Gets or sets the image data.
        /// </summary>
        public Stream MediaFileData { get; set; }

        /// <summary>
        ///     Gets or sets ParentObjectType.
        /// </summary>
        [TypeConstraint]
        public virtual ObjectType ParentObjectType { get; set; }

        /// <summary>
        ///     Gets or sets ParentObjectID.
        /// </summary>
        public virtual Guid? ParentObjectID { get; set; }

        /// <summary>
        /// Gets or sets the order to show images
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the type of the media.
        /// </summary>
        /// <value>
        /// The media type object.
        /// </value>
        public MediaType MediaType { get; set; }

        #endregion
    }
}