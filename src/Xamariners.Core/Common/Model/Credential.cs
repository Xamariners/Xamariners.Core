using Xamariners.RestClient.Helpers.Models;

namespace Xamariners.Core.Common.Model
{
    public class Credential
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the auth token.
        /// </summary>
        public AuthToken AuthToken { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user subject role.
        /// </summary>
        public string UserRole { get; set; }

        /// <summary>
        /// Gets or sets the user subject Id.
        /// </summary>
        public string UserID { get; set; }

        #endregion
    }
}
