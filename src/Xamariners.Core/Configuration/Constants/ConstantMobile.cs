
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstantMobile.cs" company="Xamariners ">
//     All code copyright Xamariners . all rights reserved
//     Date: 22 01 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using CommonServiceLocator;
using PCLStorage;
using Xamariners.Core.Common;

namespace Xamariners.Core.Configuration.Constants
{
    /// <summary>
    ///     The constant.
    /// </summary>
    public static partial class Constant
    {
        private static string _localApplicationDataRoot;

        /// <summary>
        /// in case any full (long) error seeps through, force friendly message 
        /// </summary>
      
        public static string LocalApplicationDataRoot
        {
            get
            {
                if (string.IsNullOrEmpty(_localApplicationDataRoot))
                {
                    _localApplicationDataRoot = ServiceLocator.Current.GetInstance<IFileSystem>().LocalStorage.Path;
                }

                return _localApplicationDataRoot;
            }

            set
            {
                _localApplicationDataRoot = value;
            }
        }
        public static int CACHE_PERSISTANCE_TIMER = 20;
        
        /// <summary>
        /// Number of seconds without connectivity before we show the dialog
        /// </summary>
        public static int NO_INTERNET_TOLERANCE = 2;

        /// <summary>
        /// How often (in seconds) to check for connectivity
        /// </summary>
        public static int NO_INTERNET_CHECK_FREQUENCY = 5;
    }
}