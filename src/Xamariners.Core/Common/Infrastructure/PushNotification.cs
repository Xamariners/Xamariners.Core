// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PushNotification.cs" company="">
//   
// </copyright>
// <summary>
//   The push notification.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Xamariners.Core.Common.Enum;

namespace Xamariners.Core.Common
{
    using System.ComponentModel;

    using Xamariners.Core.Common;
    using Xamariners.Core.Common.Helpers;

    /// <summary>
    /// The push notification.
    /// </summary>
    public class PushNotification : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The _has been processed.
        /// </summary>
        private bool _hasBeenProcessed;

        #endregion

        #region Public Events

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the badge.
        /// </summary>
        public int Badge { get; set; }

        /// <summary>
        /// Gets or sets the device type.
        /// </summary>
        public DeviceType DeviceType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether from launching.
        /// </summary>
        public bool FromLaunching { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has been processed.
        /// </summary>
        public bool HasBeenProcessed
        {
            get
            {
                return _hasBeenProcessed;
            }

            set
            {
                _hasBeenProcessed = value;
                PropertyChanged.Raise(() => HasBeenProcessed);
            }
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the raw notification.
        /// </summary>
        public object RawNotification { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether show banner.
        /// </summary>
        public bool ShowBanner { get; set; }

        /// <summary>
        /// Gets or sets the sound.
        /// </summary>
        public string Sound { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether was app running.
        /// </summary>
        public bool WasAppRunning { get; set; }

        //public ViewModel OnTapViewModel { get; set; } 
        #endregion
    }
}