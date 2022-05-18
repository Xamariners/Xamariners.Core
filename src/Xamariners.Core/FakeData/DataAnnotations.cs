// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Attributes.cs" company="">
//   
// </copyright>
// <summary>
//   The not mapped attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace System.ComponentModel.DataAnnotations.Schema
{
    /// <summary>
    /// The not mapped attribute.
    /// </summary>
    //public class NotMappedAttribute : Attribute
    //{
    //    // this does nothing
    //}

    /// <summary>
    /// The non serialized attribute.
    /// </summary>
    public class NonSerializedAttribute : Attribute
    {
        // this does nothing
    }

    /// <summary>
    /// The foreign key attribute.
    /// </summary>
    public class ForeignKeyAttribute : Attribute
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ForeignKeyAttribute"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public ForeignKeyAttribute(string key)
        {
        }

        #endregion

        public string Name { get; set; }
    }

    /// <summary>
    /// The key attribute.
    /// </summary>
    public class KeyAttribute : Attribute
    {
    }
}