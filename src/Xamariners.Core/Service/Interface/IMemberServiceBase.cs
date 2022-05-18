// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMemberService.cs" company="">
//   
// </copyright>
// <summary>
//   The i member service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Xamariners.Core.Common.Enum;
using Xamariners.Core.Model;
using Xamariners.Core.Service.Interface.Requests;
using Xamariners.RestClient.Helpers.Models;

namespace Xamariners.Core.Service.Interface
{
    /// <summary>
    ///     The i member service.
    /// </summary>
    public interface IMemberServiceBase<T> : IService<T> where T : MemberBase
    {
        #region Public Methods and Operators

        /// <summary>
        /// The authenticate.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="IMember"/>.
        /// </returns>
        //ServiceResponse<T> Authenticate(string username = "");

        /// <summary>
        /// The authenticate by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="MemberBase"/>.
        /// </returns>
        ServiceResponse<T> AuthenticateById(Guid id);

        /// <summary>
        /// The get by username.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResponse"/>.
        /// </returns>
        ServiceResponse<T> GetByUsername(string username);

        /// <summary>
        /// The register device token.
        /// </summary>
        /// <param name="memberId">
        /// The member id.
        /// </param>
        /// <param name="deviceToken">
        /// The device token.
        /// </param>
        /// <param name="deviceType">
        /// The device type.
        /// </param>
        /// <returns>
        /// The <see cref="ServiceResponse"/>.
        /// </returns>
        Task<ServiceResponse<string>> RegisterDeviceToken(Guid memberId, string deviceToken, DeviceType deviceType);

        ServiceResponse<bool> DeleteIdentityByAuthIdentifier(AuthIdentifierRequest authIdentifierRequest);

        #endregion
    }
}