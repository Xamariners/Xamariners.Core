// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeDisposable.cs" company="Xamariners ">
//     All code copyright Xamariners . all rights reserved
//     Date: 06 08 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Xamariners.Core.FakeData
{
    using System;

    /// <summary>
    /// The disposable.
    /// </summary>
    public class FakeDisposable : IDisposable
    {
        public FakeDisposable()
        {
            
        }

        #region Constants and Fields

        /// <summary>
        /// The is disposed.
        /// </summary>
        private bool isDisposed;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Finalizes an instance of the <see cref="FakeDisposable"/> class. 
        /// Finalizes an instance of the Disposable class. 
        /// </summary>
        ~FakeDisposable()
        {
            Dispose(false);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The dispose core.
        /// </summary>
        protected virtual void DisposeCore()
        {
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                DisposeCore();
            }

            isDisposed = true;
        }

        #endregion
    }
}