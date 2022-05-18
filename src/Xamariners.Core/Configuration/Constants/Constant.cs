// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Constant.cs" company="">
//   
// </copyright>
// <summary>
//   The Config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Xamariners.Core.Configuration.Constants
{
    /// <summary>
    ///     The Config.
    /// </summary>
    public partial class Constant
    {
        #region Constants

        /// <summary>
        ///     The anonymous username.
        /// </summary>
        public const string ANONYMOUS_USERNAME = "Anonymous@xamariners.com";

        /// <summary>
        ///     The anonymous password.
        /// </summary>
        public static readonly string ANONYMOUS_PASSWORD = ".,!.,!xamariners";

        /// <summary>
        ///     The anonymous id.
        /// </summary>
        public static readonly Guid ANONYMOUS_ID = new Guid("11111111-1111-1111-1111-111111111111");


        /// <summary>
        ///     The defaul t_ datetime.
        /// </summary>
        public static readonly DateTime DEFAULT_DATETIME = new DateTime(1, 1, 1, 0, 0, 0);


        /// <summary>
        ///     The asyn c_ reques t_ timeout.
        /// </summary>
        public const int ASYNC_REQUEST_TIMEOUT = 4 * 60 * 1000;

        #endregion

        #region Static Fields

        public static ConfigurationType CONFIGURATION_TYPE { get; set; }

        public static List<string> ACCEPTED_COUNTRY_CODES = new List<string> { "US", "FR", "GB", "SG", "PT", "ES" };

        public static string USERNAME_TEMP_MASK = "{0}@{1}.temp.xamariners.com";

        public static string USERNAME_RESET_MASK = "{0}@reset.xamariners.com";

        // {identifier}@{authtype}.xamariners.com
        public static string USERNAME_MASK = "{0}@{1}.xamariners.com";

        public static string DEFAULT_PASSWORD_TEMPMEMBER = ".,!.,!xamariners";

        #endregion
    }
}