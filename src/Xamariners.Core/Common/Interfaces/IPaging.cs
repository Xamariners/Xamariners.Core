// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPaging.cs" company="Xamariners ">
//     All code copyright Xamariners . all rights reserved
//     Date: 06 08 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Xamariners.Core.Common.Interfaces
{
    /// <summary>
    ///     Interface for paging of data.
    /// </summary>
    public interface IPaging
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets CurrentPage.
        /// </summary>
        int CurrentPage { get; set; }

        /// <summary>
        ///     Gets or sets PageSize.
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        ///     Gets or sets TotalItems.
        /// </summary>
        int TotalItems { get; set; }

        #endregion
    }
}