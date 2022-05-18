// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMediaFileInfoService.cs" company="">
//   
// </copyright>
// <summary>
//   The IImageService service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Xamariners.Core.Common.Enum;
using Xamariners.Core.Model;
using  Xamariners.Core.Model.Enums;
using Xamariners.Core.Requests;
using Xamariners.RestClient.Helpers.Models;

namespace Xamariners.Core.Service.Interface
{
    using System.IO;

    /// <summary>
    ///     The IImageService service.
    /// </summary>
    public interface IMediaFileInfoService<T> : IService<T> where T : IMediaFileInfo
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get image data by prefix.
        /// </summary>
        /// <param name="fakeMediaNamePrefix">
        /// The fake image name prefix.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        List<Stream> GetMediaDataByPrefix(string fakeMediaNamePrefix);

        /// <summary>
        /// Gets all image identifiers
        /// </summary>
        /// <param name="parentObjectType">
        /// Object type
        /// </param>
        /// <param name="parentObjectId">
        /// Container id
        /// </param>
        /// <returns>
        /// List of all image identifiers
        /// </returns>
        ServiceResponse<List<Tuple<Guid, MediaContainer>>> GetMediaIdentifiers(ObjectType parentObjectType, Guid parentObjectId);

        /// <summary>
        /// Gets images identifiers filtered by image container
        /// </summary>
        /// <param name="parentObjectType">
        /// Object type
        /// </param>
        /// <param name="parentObjectId">
        /// Container id
        /// </param>
        /// <param name="container">
        /// Image container
        /// </param>
        /// <returns>
        /// List of image identifiers filtered by image container
        /// </returns>
        ServiceResponse<List<Tuple<Guid, MediaContainer>>> GetMediaIdentifiersByContainer(
            ObjectType parentObjectType, 
            Guid parentObjectId, 
            MediaContainer container);

        ServiceResponse<IEnumerable<T>> GetByParentObjectId(Guid parentObjectId, ObjectType parentObjectType);

        ServiceResponse<IEnumerable<T>> GetByParentObjectIdList(IEnumerable<Guid> ids, ObjectType parentObjectType);
        
        /// <summary>
        /// The upload.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <param name="mediaStorePath">
        /// The media Store Path.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResponse{T}"/>.
        /// </returns>
        ServiceResponse<string> Upload(MediaRequest image);
       

        /// <summary>
        /// The empty azure storage container.
        /// </summary>
        /// <param name="parentObjectType">
        /// The object type.
        /// </param>
        /// <param name="mediaStorePath">
        /// The media store path.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResponse"/>.
        /// </returns>
        ServiceResponse<bool> EmptyStorageContainer(string parentObjectType, string mediaStorePath);

        ServiceResponse<T> Delete(MediaRequest imageRequest);

        Stream GetImageData(Guid imageId);

        #endregion
    }
}