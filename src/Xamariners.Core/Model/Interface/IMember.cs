using System.Collections.Generic;
using Xamariners.Core.Common.Enum;

namespace Xamariners.Core.Model.Interface
{
    /// <summary>
    ///     The i member.
    /// </summary>
    public interface IMember : ICoreObject
    {
        #region Public Properties
        
        /// <summary>
        ///     Gets or sets NewPassword.
        /// </summary>
        string NewPassword { get; set; }

        /// <summary>
        ///     Gets or sets Password.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        ///     Gets or sets Text.
        /// </summary>
        string Username { get; set; }

        /// <summary>
        ///     Gets or sets UserRole.
        /// </summary>
        UserRole UserRole { get; set; }
       
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets FirstName.
        /// </summary>
        string LastName { get; set; }

        List<MediaFileInfo> Media { get; set; }

        #endregion
    }
}