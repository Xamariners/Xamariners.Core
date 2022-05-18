// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILocalAuthenticationManager.cs" company="">
//   
// </copyright>
// <summary>
//   The LocalAuthenticationManager interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Xamariners.Core.Model.Enums;
using Xamariners.RestClient.Helpers.Models;

namespace Xamariners.Core.Mobile.App.Interfaces
{
    /// <summary>
    /// The LocalAuthenticationManager interface.
    /// </summary>
    public interface ILocalAuthenticationManager<TMember>
    {
        #region Public Methods and Operators

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="credentials">
        /// The credentials.
        /// </param>
        void Register(Credentials credentials);

        /// <summary>
        /// The sign in.
        /// </summary>
        /// <param name="credentials">
        /// The credentials.
        /// </param>
        /// <param name="startUpdaters"></param>
        void SignIn(Credentials credentials, bool startUpdaters);

        /// <summary>
        /// The sign in.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        void SignIn(AuthenticationProvider provider);

        /// <summary>
        /// The sign out.
        /// </summary>
        void SignOut();

        #endregion
    }
}