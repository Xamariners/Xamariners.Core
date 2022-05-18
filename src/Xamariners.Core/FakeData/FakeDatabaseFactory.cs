// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeDatabaseFactory.cs" company="Xamariners ">
//     All code copyright Xamariners . all rights reserved
//     Date: 06 08 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using Xamariners.Core.Common;
using Xamariners.Core.Interface;

namespace Xamariners.Core.FakeData
{
    /// <summary>
    /// The database factory.
    /// </summary>
    public class FakeDatabaseFactory : FakeDisposable, IDatabaseFactory
    {
        #region Constants and Fields

        /// <summary>
        /// The data context.
        /// </summary>
        private readonly IContext _context;

        #endregion

        #region Public Methods

        public FakeDatabaseFactory(IContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="IContext"/>.
        /// </returns>
        public IContext GetContext()
        {
            return _context;
        }

        public IContext CreateNewContext()
        {
            return _context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The dispose core.
        /// </summary>
        protected override void DisposeCore()
        {
            //_context = null;
        }

        #endregion
    }
}