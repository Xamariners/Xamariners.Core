// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Constant.cs" company="">
//   
// </copyright>
// <summary>
//   The constant.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Xamariners.Common
{
    /// <summary>
    ///     The constant.
    /// </summary>
    public static partial class Constant
    {
        /// <summary>
        ///     The anonymous password.
        /// </summary>
        public const string ANONYMOUS_PASSWORD = "Anonymous";

        /// <summary>
        ///     The anonymous username.
        /// </summary>
        public const string ANONYMOUS_USERNAME = "Anonymous";

        /// <summary>
        ///     The anonymous id.
        /// </summary>
        public static Guid ANONYMOUS_ID = new Guid("11111111-1111-1111-1111-111111111111");

        /// <summary>
        ///     The anti forgery token salt.
        /// </summary>
        public const string AntiForgeryTokenSalt = "1QaZ2WsX3EdC4RfV5TgB6YhN7UjM8Ik<9Ol>0P;?-{'=]~";

        /// <summary>
        /// The db file (obsolete for now, but to be injected in ioc if required)
        /// </summary>
        public const string DB_FILE = "Xamariners.db";

        /// <summary>
        ///     The rest api endpoint (where the app is fetching data from)
        /// </summary>
        //public const string RESTAPI_ENDPOINT = "http://api.helloroom.com/api/";
		public const string RESTAPI_ENDPOINT = "http://localhost/api/";
        //public const string RESTAPI_ENDPOINT = "http://apiuat.helloroom.com/api/";

        /// <summary>
        /// the local MEDIASTORE_PATH (images, video, etc)
        /// </summary>
        public static readonly string MEDIASTORE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"/MediaStore";

        /// <summary>
        /// FRIENDLY_ERROR switch ( off for debugging, on for production)
        /// </summary>
        public static bool FRIENDLY_ERROR = false;

        /// <summary>
        /// POPULATE FAKE MEDIASTORE (for not fake config)
        /// </summary>
        public static bool POPULATE_FAKEMEDIASTORE = true;

        /// <summary>
        /// Shows the admin details on the login screen
        /// </summary>
        public static bool ADMIN_INFO = true;

        public const int ASYNC_REQUEST_TIMEOUT = 20 * 1000;
        /// <summary>
        /// The cache expiration span
        /// </summary>
        public static string CACHING_TIMETOLIVE = "7:0:0:0";

        /// <summary>
        /// The caching diskpath for disk based caching
        /// </summary>
        public static string CACHING_DISKPATH = "";

        /// <summary>
        /// The caching type (BTreeCache, InProcessMemoryCache, NoCache, OutOfProcessMemoryCache, SiaqoDbCache)
        /// </summary>
        public static string CACHING_CACHETYPE = "Xamariners.Caching.SiaqoDBCache";
    }
}