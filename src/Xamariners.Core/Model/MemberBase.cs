// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Member.cs" company="">
//   
// </copyright>
// <summary>
//   The member.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Runtime.Serialization;
using Xamariners.Core.Common.Enum;
using Xamariners.Core.Model.Attributes;
using Xamariners.Core.Model.Interface;
using Xamariners.Core.Model.Internal;

namespace Xamariners.Core.Model
{
    

    /// <inheritdoc />
    /// <summary>
    ///     The member.
    /// </summary>
    public partial class MemberBase : CoreObject, IMember
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MemberBase" /> class.
        /// </summary>
        public MemberBase()
        {
            Media = new List<MediaFileInfo>();
        }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets FirstName.
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        ///     Gets the full name.
        /// </summary>
        public virtual string FullName
        {
            get => FirstName + " " + LastName;
            set
            {
                // do nothing
                var dummy = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets LastName.
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        ///     Gets or sets Images.
        /// </summary>
        public virtual List<MediaFileInfo> Media { get; set; }
   
        // <summary>
        /// <summary>
        ///     Gets or sets the middle name.
        /// </summary>
        public string MiddleName { get; set; }

        [JsonSerialise(UserRole.None)]
        public virtual string NewPassword { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets Password.
        ///     This is only used for fake repository use and repository population, not production!
        /// </summary>
        [JsonSerialise(UserRole.None)]
        public virtual string Password { get; set; }

        /// <summary>
        ///     Gets or sets Title.
        /// </summary>
        public string Title { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets Text.
        /// </summary>
        public virtual string Username { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets UserRole.
        /// </summary>
        public virtual UserRole UserRole { get; set; }

        #endregion
    }
}