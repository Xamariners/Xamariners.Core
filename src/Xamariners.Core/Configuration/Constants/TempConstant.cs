// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TempConstant.cs" company="">
//   
// </copyright>
// <summary>
//   The temp constant.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Xamariners.Core.Configuration.Constants
{
    /// <summary>
    ///     The temp constant.
    /// </summary>
    public class TempConstant
    {
        #region Constants

        /// <summary>
        ///     The aut h_ toke n_ lif e_ hours.
        /// </summary>
        public const int AUTH_TOKEN_LIFE_HOURS = 24;

        #endregion

        #region Public Properties
        

        /// <summary>
        ///     Gets or sets the local application data root.
        /// </summary>
        public static string LocalApplicationDataRoot { get; set; }

        /// <summary>
        ///     Gets the mediastor e_ path.
        /// </summary>
        public static string MEDIASTORE_PATH
        {
            get
            {
                // this is to cater for DebugFake mode
                if (Constant.CONFIGURATION_TYPE == ConfigurationType.FAKE)
                {
                    return "MediaStore";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        #endregion
    }
}