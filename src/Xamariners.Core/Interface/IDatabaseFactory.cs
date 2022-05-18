// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDatabaseFactory.cs" company="Xamariners ">
//     All code copyright Xamariners . all rights reserved
//     Date: 06 08 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Xamariners.Core.Interface
{
    /// <summary>
    /// The i database factory.
    /// </summary>
    public interface IDatabaseFactory : IDisposable
    {
        #region Public Methods

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="IContext"/>.
        /// </returns>
        IContext GetContext();

        IContext CreateNewContext();

        #endregion
    }
}