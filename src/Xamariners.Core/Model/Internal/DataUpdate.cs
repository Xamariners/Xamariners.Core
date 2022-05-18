// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataUpdate.cs" company="">
//
// </copyright>
// <summary>
//   The tag.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace Xamariners.Core.Model.Internal
{
    [DataContract]
    public class DataUpdate
    {
        /// <summary>
        /// Gets or sets the member id.
        /// </summary>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the object type.
        /// </summary>
        [DataMember]
        public object Payload { get; set; }

        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        public DateTime Created { get; set; }

        public DataUpdate()
        {
            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;
        }
    }
}