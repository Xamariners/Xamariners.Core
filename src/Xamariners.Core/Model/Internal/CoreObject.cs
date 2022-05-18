// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreObject.cs" company="Xamariners ">
//     All code copyright Xamariners . all rights reserved
//     Date: 06 08 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Xamariners.Core.Common.Enum;
using Xamariners.Core.Model.Attributes;
using Xamariners.Core.Model.Interface;

namespace Xamariners.Core.Model.Internal
{
    /// <summary>
    ///     Base Xamariners Object
    /// </summary>
    public abstract class CoreObject : object, ICoreObject
    {
        /// <summary>
        ///     Gets or sets object id.
        /// </summary>
        /// <value>
        ///     From object  id.
        /// </value>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the memberId that created this object
        /// </summary>
        [JsonSerialise(UserRole.Xamariners)]
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the member username that created this object
        /// </summary>
        [JsonSerialise(UserRole.Xamariners)]
        public string CreatedByUsername { get; set; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        [JsonSerialise(UserRole.Xamariners)]
        public DateTime? Updated { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreObject"/> class.
        /// </summary>
        public CoreObject()
        {
            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;
        }

        /// <summary>
        ///     Gets or sets CreatedByRole.
        /// </summary>
        [JsonSerialise(UserRole.Xamariners)]
        public UserRole? CreatedByRole { get; set; }
    }
}