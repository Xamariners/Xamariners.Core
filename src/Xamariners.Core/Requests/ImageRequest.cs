using Xamariners.Core.Model;

namespace Xamariners.Core.Requests
{
    public class MediaRequest : RequestBase
    {
        /// <summary>
        /// Gets or sets the image information.
        /// </summary>
        /// <value>
        /// The media file info object.
        /// </value>
        public MediaFileInfo ImageInfo { get; set; }

        /// <summary>
        /// Gets or sets the image data.
        /// </summary>
        /// <value>
        /// The image data in byte array.
        /// </value>
        public byte[] ImageData { get; set; }

        /// <summary>
        /// Gets or sets the media store path.
        /// </summary>
        /// <value>
        /// The media store path string.
        /// </value>
        public string MediaStorePath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is public.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is public; otherwise, <c>false</c>.
        /// </value>
        public bool IsPublic { get; set; }
    }
}
