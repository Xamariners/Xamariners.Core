// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Paging.cs" company="">
//   
// </copyright>
// <summary>
//   Implementation of IPaging interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Xamariners.Core.Common.Interfaces;

namespace Xamariners.Common.Paging
{
    /// <summary>
    /// Implementation of IPaging interface.
    /// </summary>
    public class Paging : IPaging
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets CurrentPage.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets PageSize.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets TotalItems.
        /// </summary>
        public int TotalItems { get; set; }

        #endregion
    }
}