// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Exceptions.cs" company="">
//   
// </copyright>
// <summary>
//   Handles javascript errors
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Xamariners.Core.Common.Helpers;

namespace Xamariners.Core.Common
{
    /// <summary>
    ///     Handles javascript errors
    /// </summary>
    public class JavaScriptException : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JavaScriptException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public JavaScriptException(string message)
            : base(message)
        {
        }

        #endregion
    }

    /// <summary>
    ///     Handles OnDeleteObjectHasParentException errors
    /// </summary>
    public class OnDeleteObjectHasParentException : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OnDeleteObjectHasParentException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public OnDeleteObjectHasParentException(string message)
            : base(message)
        {
        }

        #endregion
    }

    /// <summary>
    ///     Handles OnDeleteObjectHasParentException errors
    /// </summary>
    public class NetworkException : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkException"/> class.
        ///     Initializes a new instance of the <see cref="OnDeleteObjectHasParentException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner Exception.
        /// </param>
        public NetworkException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NetworkException(string message)
            : base(message)
        {
        }

        public NetworkException()
            : base()
        {
        }

        #endregion
    }
    
}