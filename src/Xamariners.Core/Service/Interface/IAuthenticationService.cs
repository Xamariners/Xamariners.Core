// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuthenticationService.cs" company="">
//   
// </copyright>
// <summary>
//   The AuthenticationService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Xamariners.Core.Model.Internal;

namespace Xamariners.Core.Service.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Xamariners.Core.Model;

    /// <summary>
    ///     The AuthenticationService interface.
    /// </summary>
    public interface IAuthenticationService
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The get external login providers.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        IEnumerable<ExternalLogin> GetExternalLoginProviders();

        /// <summary>
        /// The register external.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task RegisterExternal(string username);

        #endregion
    }
}